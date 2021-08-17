using Manager.API.Token;
using Manager.API.Utils;
using Manager.API.ViewModels;
using Manager.API.ViewModels.Users;
using Manager.Core.Exceptions;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Manager.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserServices _userServices;
        private readonly IConfiguration _configuration;
        public AuthController(ITokenGenerator tokenGenerator, IUserServices userServices, IConfiguration configuration)
        {
            _tokenGenerator = tokenGenerator;
            _userServices = userServices;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userServices.GetByEmail(model.Email);

                if (user != null)
                {
                    if (user.Password == model.Password)
                    {

                        return Ok(new ResultViewModel
                        {
                            Message = "Usuário autenticado com sucesso",
                            Success = true,
                            StatusCode = HttpStatusCode.OK,
                            Data = new
                            {
                                Token = _tokenGenerator.GenerateToken(user.Name),
                                TokenExpiresTime = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"]))
                            }
                        });
                    }
                }

                return Unauthorized(new ResultViewModel
                {
                    Message = "Usuário não autenticado, Email/Senha incorreto.",
                    Success = false,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Data = null
                });
            }
            catch(DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch(Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}
