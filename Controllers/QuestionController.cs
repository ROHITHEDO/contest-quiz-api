using ContestSystem.Data;
using ContestSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ContestSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "VIP,Admin")]
        [HttpPost("add-question")]
        public IActionResult AddQuestion(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();

            return Ok("Question added");
        }

        [Authorize(Roles = "VIP,Admin")]
        [HttpPost("add-option")]
        public IActionResult AddOption(Option option)
        {
            _context.Options.Add(option);
            _context.SaveChanges();

            return Ok("Option added");
        }


    }
}
