using KnowledgeManagementSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto?> GetUser(int id);
        Task<UserProfileDto?> GetUserProfile(int id);
        Task<int> AddUser(RegisterUserDto userDto);
        Task<bool> UpdateUser(UpdateUserDto userDto);
        Task<bool> DeleteUser(int id);
        Task<UserDto?> GetUserByNameAndSurname(string name, string surname);
        Task<IEnumerable<UserDto>> GetUsersByRole(int roleId);
        Task<IEnumerable<UserDto>> GetUsersByBirthDate(DateTime date);
    }
}
