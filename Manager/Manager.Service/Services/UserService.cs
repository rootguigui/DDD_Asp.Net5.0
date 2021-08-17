using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Services.Services
{
    public class UserService : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByEmail(userDTO.Email);

            if (userExists != null) throw new DomainException("Já existe um usuário cadastro com o email informado.");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();

            return _mapper.Map<UserDTO>(await _userRepository.Create(user));
        }

        public async Task<UserDTO> Delete(long id)
        {
            var userExists = await _userRepository.Get(id);

            if (userExists == null) throw new DomainException("Usuário não pode ser encontrado");

            await _userRepository.Delete(id);

            return _mapper.Map<UserDTO>(userExists);
        }

        public async Task<UserDTO> Get(long id) => _mapper.Map<UserDTO>(await _userRepository.Get(id));
        
        public async Task<List<UserDTO>> Get() => _mapper.Map<List<UserDTO>>(await _userRepository.Get());

        public async Task<UserDTO> GetByEmail(string email)
        {
            if (String.IsNullOrEmpty(email)) throw new DomainException("E-mail não pode ser nulo ou invalido");

            var user = await _userRepository.GetByEmail(email);

            if (user == null) throw new DomainException("Usuário não encontrado!");

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            if (String.IsNullOrEmpty(email)) throw new DomainException("E-mail não pode ser nulo ou invalido");

            var user = await _userRepository.SearchByEmail(email);

            return _mapper.Map<List<UserDTO>>(user);
        }

        public async Task<List<UserDTO>> SearchByName(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new DomainException("O nome do usuário nulo ou vazio.");

            var user = await _userRepository.SearchByName(name);

            if (user == null)
            {
                throw new DomainException("Usuário não encontrado");
            }

            return _mapper.Map<List<UserDTO>>(user);
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var userExists = await _userRepository.Get(userDTO.Id);

            if (userExists == null) throw new DomainException("Usuário não encontrado!");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();

            var userUpdated = await _userRepository.Update(user);

            return _mapper.Map<UserDTO>(userUpdated);
        }
    }
}
