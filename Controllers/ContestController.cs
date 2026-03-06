using ContestSystem.Data;
using ContestSystem.DTOs;
using ContestSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContestSystem.Controllers
{
    [ApiController]
    [EnableRateLimiting("fixed")]
    [Route("api/[controller]")]
    public class ContestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContestController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "VIP,Admin")]
        [HttpPost("create")]
        public IActionResult CreateContest([FromBody] Contest contest)
        {
            _context.Contests.Add(contest);
            _context.SaveChanges();

            return Ok("Contest created");
        }

        [AllowAnonymous]
        [HttpGet("view-contests")]
        public IActionResult GetContests()
        {
            var contests = _context.Contests.ToList();
            return Ok(contests);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetContest(int id)
        {
            var contest = _context.Contests
                .Include(x => x.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefault(x => x.Id == id);

            if (contest == null)
                return NotFound();

            return Ok(contest);
        }

        [Authorize(Roles = "Admin,VIP,Normal")]
        [HttpPost("join/{contestId}")]
        public IActionResult JoinContest(int contestId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var participation = new ContestParticipation
            {
                UserId = userId,
                ContestId = contestId,
                Score = 0,
                IsSubmitted = false
            };

            _context.Participations.Add(participation);
            _context.SaveChanges();

            return Ok("Joined contest");
        }

        [Authorize]
        [HttpPost("submit/{contestId}")]
        public IActionResult SubmitAnswers(int contestId, List<AnswerDto> answers)
        {
            int score = 0;

            foreach (var answer in answers)
            {
                var correctOptions = _context.Options
                    .Where(x => x.QuestionId == answer.QuestionId && x.IsCorrect)
                    .Select(x => x.Id)
                    .ToList();

                if (!correctOptions.Except(answer.SelectedOptionIds).Any() &&
                    !answer.SelectedOptionIds.Except(correctOptions).Any())
                {
                    score++;
                }
            }

            var participation = _context.Participations
                .FirstOrDefault(x => x.ContestId == contestId);

            participation.Score = score;
            participation.IsSubmitted = true;

            _context.SaveChanges();

            return Ok(new { Score = score });
        }

   
        [HttpGet("leaderboard/{contestId}")]
        public IActionResult Leaderboard(int contestId)
        {
            var leaderboard = _context.Participations
                .Where(x => x.ContestId == contestId && x.IsSubmitted)
                .OrderByDescending(x => x.Score)
                .Take(10)
                .Select(x => new
                {
                    User = x.User.Name,
                    x.Score
                })
                .ToList();

            return Ok(leaderboard);
        }

    }
}
