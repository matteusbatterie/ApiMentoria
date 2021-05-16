using FluentValidation;

namespace ApiMentoria.Models.Validation
{
    public class UserModelValidation : AbstractValidator<UserModel>
    {
        public UserModelValidation()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("O campo nome é obrigatório.")
                .Length(10, 100).WithMessage("O campo deve possuir no mínimio 10 caracteres e no máximo 100.");
            
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("O campo e-mail é obrigatório.")
                .EmailAddress();
        }
    }
}
