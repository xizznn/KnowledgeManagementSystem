using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace KnowledgeManagementSystem.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var entities = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(entities);
        }

        public async Task<UserDto?> GetUser(int id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<UserDto>(entity);
        }

        public async Task<UserProfileDto?> GetUserProfile(int id)
        {
            var entity = await _userRepository.GetUserWithDetailsAsync(id);
            return entity == null ? null : _mapper.Map<UserProfileDto>(entity);
        }

        public async Task<int> AddUser(RegisterUserDto userDto)
        {
            var entity = _mapper.Map<UserEntity>(userDto);

            using (var hmac = new HMACSHA512())
            {
                entity.PasswordSalt = hmac.Key;
                entity.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));
            }

            await _userRepository.AddAsync(entity);
            return entity.Id;
        }

        public async Task<bool> UpdateUser(UpdateUserDto userDto)
        {
            var entity = await _userRepository.GetByIdAsync(userDto.Id);
            if (entity == null) return false;

            if (!string.IsNullOrEmpty(userDto.Name))
                entity.Name = userDto.Name;

            if (!string.IsNullOrEmpty(userDto.Surname))
                entity.Surname = userDto.Surname;

            if (!string.IsNullOrEmpty(userDto.Email))
                entity.Email = userDto.Email;

            if (userDto.DateOfBirth.HasValue)
                entity.DateOfBirth = userDto.DateOfBirth.Value;

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                using var hmac = new System.Security.Cryptography.HMACSHA512();
                entity.PasswordSalt = hmac.Key;
                entity.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userDto.Password));
            }

            entity.RoleId = userDto.RoleId;

            await _userRepository.UpdateAsync(entity);
            return true;
        }



        public async Task<bool> DeleteUser(int id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            if (entity == null) return false;
            await _userRepository.RemoveAsync(entity);
            return true;
        }

        public async Task<UserDto?> GetUserByNameAndSurname(string name, string surname)
        {
            var entity = await _userRepository.GetByNameAndSurnameAsync(name, surname);
            return entity == null ? null : _mapper.Map<UserDto>(entity);
        }

        public async Task<IEnumerable<UserDto>> GetUsersByRole(int roleId)
        {
            var entities = await _userRepository.GetUsersByRoleAsync(roleId);
            return _mapper.Map<IEnumerable<UserDto>>(entities);
        }

        public async Task<IEnumerable<UserDto>> GetUsersByBirthDate(DateTime date)
        {
            var entities = await _userRepository.GetUsersByBirthDateAsync(date);
            return _mapper.Map<IEnumerable<UserDto>>(entities);
        }
    }
}
