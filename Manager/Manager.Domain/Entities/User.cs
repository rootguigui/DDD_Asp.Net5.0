using Manager.Core.Exceptions;
using Manager.Domain.Validators;
using System;

namespace Manager.Domain.Entities
{
    public class User : Base
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        protected User() { }

        public User(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        public void changeName(string name)
        {
            this.Name = name;
            Validate();
        }

        public void changePassword(string password)
        {
            this.Password = password;
            Validate();
        }

        public void changeEmail(string email)
        {
            this.Email = email;
            Validate();
        }

        public override bool Validate()
        {
            var validator = new UserValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach(var error in validation.Errors)
                {
                    _errors.Add(error.ErrorMessage);
                }

                throw new DomainException("Alguns campos  estão inválidos, por favor corrija-os", _errors);
            }

            return true;
        }
    }
}
