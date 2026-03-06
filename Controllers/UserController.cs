using ContestSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContestSystem.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var history = _context.Participations
                .Where(x => x.UserId == userId)
                .Include(x => x.Contest)
                .Select(x => new
                {
                    Contest = x.Contest.Name,
                    x.Score,
                    x.IsSubmitted
                });

            return Ok(history);
        }
    }
}
