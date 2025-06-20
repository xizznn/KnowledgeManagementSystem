using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KnowledgeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersOnCoursesController : ControllerBase
    {
        private readonly IUserOnCourseService _userCourseService;
        private readonly IMapper _mapper;

        public UsersOnCoursesController(IUserOnCourseService userCourseService, IMapper mapper)
        {
            _userCourseService = userCourseService;
            _mapper = mapper;
        }

        [HttpGet("user/{userId:int}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Получить курсы пользователя")]
        public async Task<ActionResult<IEnumerable<UserOnCourseDto>>> GetUserCourses(int userId)
        {
            var userCourses = await _userCourseService.GetUserCourses(userId);
            var result = _mapper.Map<IEnumerable<UserOnCourseDto>>(userCourses);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Добавить пользователя в курс")]
        public async Task<IActionResult> AddUserToCourse([FromBody] CreateUserOnCourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            if (!User.IsInRole("Admin") && dto.UserId != currentUserId)
                return Forbid();

            var entity = _mapper.Map<UserOnCourseEntity>(dto);
            var result = await _userCourseService.AddUserToCourse(entity);
            return result ? Ok() : BadRequest();
        }

        [HttpDelete("{userId:int}/{courseId:int}")]
        [Authorize]
        [SwaggerOperation(Summary = "Удалить пользователя из курса")]
        public async Task<IActionResult> RemoveUserFromCourse(int userId, int courseId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            if (!User.IsInRole("Admin") && userId != currentUserId)
                return Forbid();

            var result = await _userCourseService.RemoveUserFromCourse(userId, courseId);
            return result ? NoContent() : NotFound();
        }
    }
}
