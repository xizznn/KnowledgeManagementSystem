using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Получить все роли")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            var dtos = await _service.GetAllRoles();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Получить роль по айди")]
        public async Task<ActionResult<RoleDto>> GetById(int id)
        {
            var dto = await _service.GetRole(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpGet("by-title")]
        [SwaggerOperation(Summary = "Получить роль по названию")]
        public async Task<ActionResult<RoleDto>> GetByTitle([FromQuery] string title)
        {
            var dto = await _service.GetRoleByTitle(title);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Создать новую роль")]
        public async Task<ActionResult<RoleDto>> Create([FromBody] CreateRoleDto dto)
        {
            var newId = await _service.AddRole(dto);
            var created = await _service.GetRole(newId);
            return CreatedAtAction(nameof(GetById), new { id = newId }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Обновить роль")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleDto dto)
        {
            dto.Id = id;
            var updated = await _service.UpdateRole(dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Удалить роль")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteRole(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
