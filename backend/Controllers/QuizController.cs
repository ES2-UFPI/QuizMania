using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;
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


        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Sucess </h3>
        /// <h3> **404:** OwnerNotFound </h3>
        /// <h3> **400:** BadRequest, EmptyAtribute, QuestionWithoutCorrectAnswer </h3>
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<SaveQuizResponseDTO>> PostQuiz(SaveQuiz_QuizDTO quizReceived)
        {
            var result = await _quizService.SaveQuizAsync(quizReceived);
            switch (result._result)
            {
                case SaveQuizResponseDTO.RequestResult.Success: return Ok(result);
                case SaveQuizResponseDTO.RequestResult.OwnerNotFound: return NotFound(result);
                default: return BadRequest(result);
            }
        }


        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Sucess </h3>
        /// <h3> **404:** QuizNotFound </h3>
        /// <h3> **400:** BadRequest, CharacterNotOwner </h3>
        /// </remarks>
        [HttpDelete]
        public async Task<ActionResult<DeleteQuizResponseDTO>> DeleteQuiz(DeleteQuizRequestDTO deleteRequest)
        {
            var result = await _quizService.DeleteQuizAsync(deleteRequest);
            switch (result._result)
            {
                case DeleteQuizResponseDTO.RequestResult.Success: return Ok(result);
                case DeleteQuizResponseDTO.RequestResult.QuizNotFound: return NotFound(result);
                default: return BadRequest(result);
            }
        }


        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Sucess </h3>
        /// <h3> **404:** QuizNotFound </h3>
        /// <h3> **400:** BadRequest, EmptyAtribute, QuestionWithoutCorrectAnswer, CharacterNotOwner </h3>
        /// </remarks>
        [HttpPost("question")]
        public async Task<ActionResult<QuestionReadDTO>> PostQuestion(SaveQuestion_QuestionDTO questionReceived)
        {
            var result = await _quizService.SaveQuestionAsync(questionReceived);
            switch (result._result)
            {
                case SaveQuestionResponseDTO.RequestResult.Success: return Ok(result);
                case SaveQuestionResponseDTO.RequestResult.QuizNotFound: return NotFound(result);
                default: return BadRequest(result);
            }
        }


        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Sucess </h3>
        /// <h3> **404:** QuizNotFound, QuestionNotFound </h3>
        /// <h3> **400:** BadRequest, CharacterNotOwner </h3>
        /// </remarks>
        [HttpDelete("question")]
        public async Task<ActionResult<DeleteQuestionResponseDTO>> DeleteQuestion(DeleteQuestionRequestDTO deleteRequest)
        {
            var result = await _quizService.DeleteQuestionAsync(deleteRequest);
            switch (result._result)
            {
                case DeleteQuestionResponseDTO.RequestResult.Success: return Ok(result);
                case DeleteQuestionResponseDTO.RequestResult.QuizNotFound: return NotFound(result);
                case DeleteQuestionResponseDTO.RequestResult.QuestionNotFound: return NotFound(result);
                default: return BadRequest(result);
            }
        }


        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Sucess </h3>
        /// <h3> **404:** CharacterNotFound, QuizNotFound, QuestionNotFound, AnswerNotFound </h3>
        /// <h3> **400:** BadRequest, InvalidQuizFeedback, QuizWithoutQuestions </h3>
        /// </remarks>
        [HttpPost("feedback")]
        public async Task<ActionResult<SaveQuizFeedbackResponseDTO>> PostQuizFeedback(SaveQuizFb_QuizFeedbackDTO quizFbReceived)
        {
            var result = await _quizService.SaveQuizFeedbackAsync(quizFbReceived);
            switch (result._result)
            {
                case SaveQuizFeedbackResponseDTO.RequestResult.Success: return Ok(result);
                case SaveQuizFeedbackResponseDTO.RequestResult.CharacterNotFound: return NotFound(result);
                case SaveQuizFeedbackResponseDTO.RequestResult.QuizNotFound: return NotFound(result);
                case SaveQuizFeedbackResponseDTO.RequestResult.QuestionNotFound: return NotFound(result);
                case SaveQuizFeedbackResponseDTO.RequestResult.AnswerNotFound: return NotFound(result);
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
