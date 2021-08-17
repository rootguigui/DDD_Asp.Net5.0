using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.API.ViewModels.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O e-mail não pode ser vazio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha não pode ser vazia")]
        [MinLength(6, ErrorMessage = "A senha deve conter no minimo 6 caracteres")]
        public string Password { get; set; }
    }
}
