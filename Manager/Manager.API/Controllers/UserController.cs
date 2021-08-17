using Manager.API.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Manager.Core.Exceptions;
using AutoMapper;
using Manager.Services.Interfaces;
using Manager.Services.DTO;
using Manager.API.ViewModels;
using System.Net;
using Manager.API.Utils;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Manager.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UserController(IUserServices userService, IMapper mapper)
        {
            _userServices = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel userViewModel)
        {
            try
            {
                var userDto = _mapper.Map<UserDTO>(userViewModel);

                var userCreated = await _userServices.Create(userDto);

                return Ok(new ResultViewModel()
                {
                    Message = "Usuário criado com sucesso!",
                    Success = true,
                    Data = userCreated,
                    StatusCode = HttpStatusCode.OK
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _userServices.Get();

                return Ok(new ResultViewModel
                {
                    Message = "Retornando lista de usuários",
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = users
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

        [HttpGet]
        [Authorize]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var user = await _userServices.Get(id);

                if (user != null)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Usuário encontrado com sucesso!",
                        StatusCode = HttpStatusCode.OK,
                        Success = true,
                        Data = user,
                    });
                }

                return NotFound(Responses.DomainErrorMessage("Usuário não encontrado!"));
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPut]
        [Authorize]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserViewModel userViewModel)
        {
            try
            {
                var userDto = _mapper.Map<UserDTO>(userViewModel);

                var userUpdated = await _userServices.Update(userDto);

                return Ok(new ResultViewModel
                {
                    Message = "Usuário atualizado com sucesso!",
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = userUpdated
                });
            }
            catch(DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("remove/{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                var userDto = await _userServices.Get(id);

                if (userDto != null)
                {
                    await _userServices.Delete(id);

                    return Ok(new ResultViewModel 
                    {
                        Message = "Usuário excluido com sucesso",
                        Success = true,
                        StatusCode = HttpStatusCode.OK,
                        Data = userDto
                    });
                }

                return NotFound(Responses.DomainErrorMessage("Usuário não encontrado!"));
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("get-by-email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "E-mail não pode ser nulo ou vazio",
                        Success = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = null
                    });
                }

                var user = await _userServices.GetByEmail(email);

                if (user == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Usuário não encontrado!",
                        Success = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null
                    });
                }

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso",
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = user
                });
            }
            catch(DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("get-by-name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                List<UserDTO> users = await _userServices.SearchByName(name);

                if (users.Count == 0)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum Usuário encontrado com esse nome",
                        Success = true,
                        StatusCode = HttpStatusCode.NotFound,
                        Data = users
                    });
                }

                return Ok(new ResultViewModel
                {
                    Message = "busca realizada com sucesso",
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = users
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