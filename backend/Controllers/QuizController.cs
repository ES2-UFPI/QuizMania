using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.DTOs;
using System.Threading.Tasks;
using QuizMania.WebAPI.Services;

namespace QuizMania.WebAPI.Controllers
{
    [Route("/quiz")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizReadDTO>>> GetQuizzes()
        {
            var quizzes = await _quizService.GetQuizzesAsync();
            return quizzes != null ? Ok(quizzes) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizReadDTO>> GetQuiz(long id)
        {
            var quiz = await _quizService.GetQuizAsync(id);
            return quiz != null ? Ok(quiz) : NotFound();
        }

        //POST: api/Quiz/
        [HttpPost]
        public async Task<ActionResult<QuizFeedbackReceivedDTO>> PostQuizFeedback(QuizFeedbackReceivedDTO quizReceived)
        {
            var quizFeedback = await _quizService.SaveQuizAnswer(quizReceived);
            return quizFeedback != null ? Ok(quizFeedback) : NotFound();
        }

        [HttpDelete]
        public async Task<ActionResult<DeleteQuizRequestResultDTO>> DeleteQuiz(DeleteQuizRequestDTO deleteRequest)
        {
            var result = await _quizService.DeleteQuiz(deleteRequest);
            switch (result._result)
            {
                case DeleteQuizRequestResultDTO.RequestResult.Success: return Ok(result);
                case DeleteQuizRequestResultDTO.RequestResult.QuizNotFound: return NotFound(result);
                default: return BadRequest(result);
            }
        }

        //// PUT: api/Quiz/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutQuiz(ulong id, Quiz quiz)
        //{
        //    if (id != quiz.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(quiz).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!QuizExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Quiz
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        //{
        //    _context.Quizzes.Add(quiz);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        //}

        //// DELETE: api/Quiz/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteQuiz(ulong id)
        //{
        //    var quiz = await _context.Quizzes.FindAsync(id);
        //    if (quiz == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Quizzes.Remove(quiz);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool QuizExists(ulong id)
        //{
        //    return _context.Quizzes.Any(e => e.Id == id);
        //}
    }
}
