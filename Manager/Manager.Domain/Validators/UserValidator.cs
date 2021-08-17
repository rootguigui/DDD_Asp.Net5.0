using FluentValidation;
using Manager.Domain.Entities;

namespace Manager.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A Entidade não pode ser vazia.")
                .NotNull()
                .WithMessage("a Entidade não pode ser nula.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome não pode ser vazio.")
                .NotNull()
                .WithMessage("O nome não pode ser nulo.")
                .MinimumLength(3)
                .WithMessage("O Nome deve conter no minimo 3 caracteres")
                .MaximumLength(80)
                .WithMessage("O nome deve conter no maximo 80 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("a senha não pode ser vazia.")
                .NotNull()
                .WithMessage("A senha não pode ser nula.")
                .MinimumLength(6)
                .WithMessage("a senha deve conter no minimo 6 caracteres")
                .MaximumLength(80)
                .WithMessage("a senha deve conter no maximo 80 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O e-mail não pode ser vazio.")
                .NotNull()
                .WithMessage("O email não pode ser nulo.")
                .MinimumLength(10)
                .WithMessage("O e-mail deve conter no minimo 10 caracteres")
                .MaximumLength(180)
                .WithMessage("O e-mail deve conter no maximo 180 caracteres")
                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                .WithMessage("o e-mail informado não é valido!")
                ;

        }
    }
}
