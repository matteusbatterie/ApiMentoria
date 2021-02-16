using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using ApiMentoria.Models;
using ApiMentoria.Repository.Interface;
using ApiMentoria.Service.Interface;

namespace ApiMentoria.Service
{
    public class UserService : BaseService<User>, IUserService/*: BaseService<User>, IUserRepository*/
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            this._userRepository = userRepository;
        }

        public override IEnumerable<User> Retrieve()
        {
            IEnumerable<User> users = base.Retrieve();
            return users;
        }

        public override User Retrieve(int id)
        {
            User user = base.Retrieve(id);
            return user;
        }

        public override bool Create(User user)
        {
            #region lol
            // if (user.Name.Length > 10)
            // {
            //     bool emailIsValid = false;
            //     var regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", RegexOptions.Compiled);
            //     emailIsValid = regex.IsMatch(user.Email);

            //     bool emailExists = false;
            //     IEnumerable<User> allUsers = base.Retrieve();
            //     foreach (User u in allUsers)
            //     {
            //         if (u.Email == user.Name) emailExists = true;
            //     }

            //     if (emailIsValid && !emailExists)
            //     {
            //         bool cpfIsValid = false;
            //         var cpf = user.CPF;
            //         // Referência: http://www.macoratti.net/11/09/c_val1.htm
            //         int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            //         int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            //         cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            //         if (cpf.Length != 11)
            //             cpfIsValid = false;

            //         for (int j = 0; j < 10; j++)
            //             if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
            //                 cpfIsValid = false;

            //         string tempCpf = cpf.Substring(0, 9);
            //         int sum = 0;

            //         for (int i = 0; i < 9; i++)
            //             sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            //         int remainder = sum % 11;
            //         if (remainder < 2)
            //             remainder = 0;
            //         else
            //             remainder = 11 - remainder;

            //         string digit = remainder.ToString();
            //         tempCpf = tempCpf + digit;
            //         sum = 0;
            //         for (int i = 0; i < 10; i++)
            //             sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            //         remainder = sum % 11;
            //         if (remainder < 2)
            //             remainder = 0;
            //         else
            //             remainder = 11 - remainder;

            //         digit = digit + remainder.ToString();

            //          cpfIsValid = cpf.EndsWith(digit);


            //         bool cpfExists = false;
            //         foreach (User u in allUsers)
            //         {
            //             if (u.CPF == user.CPF) cpfExists = true;
            //         }

            //         if(cpfIsValid && !cpfExists)
            //             base.Create(user);
            //     }
            // }
            #endregion

            if(!ValidateUserInfo(user))
                return false;

            return base.Create(user);
        }

        public override void Update(User user)
        {
            if (!ValidateUserInfo(user))
                return;

            base.Update(user);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }


        #region Validation
        private bool ValidateUserInfo(User user)
        {
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

        private bool EmailIsValid(string email)
        {
            if(email.Contains(" ")) return false;

            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;

            // var regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", RegexOptions.Compiled);
            // return regex.IsMatch(email);
        }
        private bool EmailExists(string email)
        {
            IEnumerable<User> allUsers = base.Retrieve();
            return allUsers.Any(user => user.Email == email);
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
        private bool CpfExists(string cpf)
        {
            bool cpfExists = false;
            IEnumerable<User> allUsers = base.Retrieve();
            foreach (User user in allUsers)
            {
                if (user.CPF == cpf) cpfExists = true;
            }

            return cpfExists;
        }
        #endregion
    }
}
