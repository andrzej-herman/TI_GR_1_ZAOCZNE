using Microsoft.AspNetCore.Mvc;
using Quiz.Api.Services;

namespace Quiz.Api.Controllers
{
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _service;

        public QuizController(IQuizService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getquestion")]
        public async Task<IActionResult> GetQuestion(int category)
        {
            var result = await _service.GetQuestionAsync(category);
            return result != null ? Ok(result) : BadRequest("Nieprawid³owa kategoria pytania");

        }

        [HttpGet]
        [Route("checkanswer")]
        public async Task<IActionResult> CheckAnswer([FromQuery] Guid answerId, [FromQuery] int category)
        {
            var result = await _service.CheckAnswerAsync(answerId, category);
            return Ok(result);
        }
    }
}
