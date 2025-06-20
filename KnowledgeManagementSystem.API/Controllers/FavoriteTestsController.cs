using AutoMapper;
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
    [Authorize]
    public class FavoritesTestsController : ControllerBase
    {
        private readonly IFavoriteTestService _favoriteService;
        private readonly IMapper _mapper;

        public FavoritesTestsController(IFavoriteTestService favoriteService, IMapper mapper)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Получить избранные тесты пользователя")]
        public async Task<ActionResult<IEnumerable<TestDto>>> GetUserFavorites()
        {
            var userId = GetUserId();
            if (userId == 0)
                return Unauthorized();

            var favorites = await _favoriteService.GetUserFavoritesTests(userId);
            return Ok(favorites);
        }

        [HttpPost("{testId}")]
        [SwaggerOperation(Summary = "Добавить тест в избранное")]
        public async Task<IActionResult> AddToFavorites(int testId)
        {
            var userId = GetUserId();
            if (userId == 0)
                return Unauthorized();

            var result = await _favoriteService.AddToFavoritesTests(userId, testId);
            if (!result)
                return BadRequest("Не удалось добавить тест в избранное");

            return Ok();
        }

        [HttpDelete("{testId}")]
        [SwaggerOperation(Summary = "Удалить тест из избранного")]
        public async Task<IActionResult> RemoveFromFavorites(int testId)
        {
            var userId = GetUserId();
            if (userId == 0)
                return Unauthorized();

            var result = await _favoriteService.RemoveFromFavoritesTests(userId, testId);
            if (!result)
                return BadRequest("Не удалось удалить тест из избранного");

            return NoContent();
        }

        [HttpGet("check/{testId}")]
        [SwaggerOperation(Summary = "Проверить, есть ли тест в избранном")]
        public async Task<ActionResult<bool>> IsTestInFavorites(int testId)
        {
            var userId = GetUserId();
            if (userId == 0)
                return Unauthorized();

            var result = await _favoriteService.IsTestInFavoritesTests(userId, testId);
            return Ok(result);
        }
    }
}
