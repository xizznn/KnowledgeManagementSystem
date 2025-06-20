using KnowledgeManagementSystem.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<RoleDto?> GetRole(int id);
        Task<RoleDto?> GetRoleByTitle(string title);
        Task<int> AddRole(CreateRoleDto dto);
        Task<bool> UpdateRole(UpdateRoleDto dto);
        Task<bool> DeleteRole(int id);
    }
}
