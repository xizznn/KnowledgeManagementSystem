using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(entities);
        }

        public async Task<RoleDto?> GetRole(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<RoleDto>(entity);
        }

        public async Task<RoleDto?> GetRoleByTitle(string title)
        {
            var entity = await _repository.GetByTitleAsync(title);
            return entity == null ? null : _mapper.Map<RoleDto>(entity);
        }

        public async Task<int> AddRole(CreateRoleDto dto)
        {
            var entity = _mapper.Map<RoleEntity>(dto);
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task<bool> UpdateRole(UpdateRoleDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteRole(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            await _repository.RemoveAsync(entity);
            return true;
        }
    }
}
