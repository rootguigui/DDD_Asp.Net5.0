using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.API.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "O e-mail não pode ser nulo.")]
        [MinLength(10, ErrorMessage = "O e-mail tem que ter no minimo 10 caracteres")]
        [MaxLength(180, ErrorMessage = "O e-mail deve conter no maximo 180 caracteres")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", 
            ErrorMessage = "O e-mail é invalido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O nome não pode ser nulo.")]
        [MinLength(3, ErrorMessage = "O nome tem que ter no minimo 3 caracteres")]
        [MaxLength(80, ErrorMessage = "O nome deve conter no maximo 80 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A senha não pode ser nulo.")]
        [MinLength(10, ErrorMessage = "A senha tem que ter no minimo 10 caracteres")]
        [MaxLength(180, ErrorMessage = "A senha deve conter no maximo 180 caracteres")]
        public string Password { get; set; }
    }
}
