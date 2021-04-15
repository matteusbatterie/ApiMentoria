using System.Linq;
using System.Collections.Generic;

using Core.Entities;
using Core.Abstractions.Service;
using Core.Abstractions.Repository;

namespace Core.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        
        public IEnumerable<User> Retrieve()
        {
            IEnumerable<User> users = _repository.GetAll();
            return users;
        }

        public User Retrieve(int id)
        {
            User user = _repository.Get(id);
            return user;
        }

        public void Create(User user)
        {
            if (!ValidateUser(user))
                return;

            _repository.Create(user);
        }

        public void Update(User user)
        {
            if (!ValidateUser(user))
                return;

            _repository.Update(user);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }


        #region Validation
        private bool ValidateUser(User user)
        {
            var a = ValidateUserEmail(user.Email);
            var b = ValidateUserCpf(user.CPF);
            var c = ValidateUserName(user.Name);
            return ValidateUserEmail(user.Email)
                && ValidateUserCpf(user.CPF)
                && ValidateUserName(user.Name);
        }

        private bool ValidateUserName(string name)
        {
            // Nome tem que ter mais de 10 caracteres (desconsiderando whitespaces)
            return name.Trim().Length > 10;
        }
        private bool ValidateUserEmail(string email)
        {
            return !EmailExists(email) && EmailIsValid(email);
        }
        private bool ValidateUserCpf(string cpf)
        {
            return !CpfExists(cpf) && CpfIsValid(cpf);
        }

        private bool EmailExists(string email)
        {
            IEnumerable<User> allUsers = _repository.GetAll();
            return allUsers.Any(user => user.Email == email);
        }

        private bool EmailIsValid(string email)
        {
            if (email.Contains(" ")) return false;

            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
        }

        private bool CpfExists(string cpf)
        {
            IEnumerable<User> allUsers = _repository.GetAll();
            return allUsers.Any(user => user.CPF == cpf);
        }

        private bool CpfIsValid(string cpf)
        {
            // Referência: http://www.macoratti.net/11/09/c_val1.htm
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            string digit = remainder.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit = digit + remainder.ToString();

            return cpf.EndsWith(digit);
        }
        #endregion
    }
}
