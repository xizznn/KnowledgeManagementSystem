using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace KnowledgeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("current-profile")]
        [SwaggerOperation(Summary = "Получить профиль текущего пользователя")]
        public async Task<IActionResult> GetCurrentProfile()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { message = "Не удалось извлечь ID пользователя из токена" });
            }

            var user = await _userService.GetUserProfile(userId);
            if (user == null)
            {
                return NotFound(new { message = "Пользователь не найден" });
            }

            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Получить всех пользователей (только для админов)")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
            => Ok(await _userService.GetAllUsers());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Получить пользователя по ID (только для админов)")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var dto = await _userService.GetUser(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpGet("{id}/profile")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Получить профиль пользователя по ID (только для админов)")]
        public async Task<ActionResult<UserProfileDto>> GetProfile(int id)
        {
            var dto = await _userService.GetUserProfile(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Зарегистрировать нового пользователя")]
        public async Task<ActionResult<UserDto>> Create([FromBody] RegisterUserDto dto)
        {
            var newId = await _userService.AddUser(dto);
            var created = await _userService.GetUser(newId);
            return CreatedAtAction(nameof(GetById), new { id = newId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Обновить данные пользователя (только для админов)")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            if (id != dto.Id && dto.Id != 0)
                dto.Id = id;

            var updated = await _userService.UpdateUser(dto);
            if (!updated)
                return NotFound(new { message = "Пользователь не найден" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Удалить пользователя (только для админов)")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteUser(id);
            if (!deleted)
                return NotFound(new { message = "Пользователь не найден" });

            return NoContent();
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Поиск пользователя по имени и фамилии (только для админов)")]
        public async Task<ActionResult<UserDto>> FindByName([FromQuery] string name, [FromQuery] string surname)
        {
            var dto = await _userService.GetUserByNameAndSurname(name, surname);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpGet("by-role/{roleId}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Получить пользователей по роли (только для админов)")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByRole(int roleId)
            => Ok(await _userService.GetUsersByRole(roleId));

        [HttpGet("by-birthdate")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Получить пользователей по дате рождения (только для админов)")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByBirthDate([FromQuery] DateTime date)
            => Ok(await _userService.GetUsersByBirthDate(date));
    }
}
