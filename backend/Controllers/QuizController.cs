using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;
using System.Threading.Tasks;
using QuizMania.WebAPI.Services;
using Microsoft.AspNetCore.Http;

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


        /// <remarks>
        /// <h2> **Description:** </h2>
        /// <h3> Retorna todos os quizzes. </h3>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuizReadDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuizzes()
        {
            var quizzes = await _quizService.GetQuizzesAsync();

            if (quizzes == null) {
                return NotFound();
            }
            
            return Ok(quizzes);
        }


        /// <remarks>
        /// <h2> **Description:** </h2>
        /// <h3> Retorna um determinado quiz. </h3>
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuizReadDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuiz(long id)
        {
            var quiz = await _quizService.GetQuizAsync(id);
            
            if (quiz == null) {
                return NotFound();
            }
            
            return Ok(quiz);
        }


        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Sucess </h3>
        /// <h3> **404:** OwnerNotFound </h3>
        /// <h3> **400:** BadRequest, EmptyAtribute, QuestionWithoutCorrectAnswer </h3>
        /// <h2> **Description:** </h2>
        /// <h3> O character especificado salva o quiz informado. </h3>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveQuizResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SaveQuizResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SaveQuizResponseDTO))]
        public async Task<IActionResult> PostQuiz(SaveQuiz_QuizDTO quizReceived)
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
        /// <h2> **Description:** </h2>
        /// <h3> Deleta o quiz especificado. </h3>
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteQuizResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DeleteQuizResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DeleteQuizResponseDTO))]
        public async Task<IActionResult> DeleteQuiz(DeleteQuizRequestDTO deleteRequest)
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
        /// <h2> **Description:** </h2>
        /// <h3> O character especificado adiciona uma nova questão ao quiz informado. </h3>
        /// </remarks>
        [HttpPost("question")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionReadDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(QuestionReadDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(QuestionReadDTO))]
        public async Task<IActionResult> PostQuestion(SaveQuestion_QuestionDTO questionReceived)
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
        /// <h2> **Description:** </h2>
        /// <h3> O character especificado deleta questão do quiz informado. </h3>
        /// </remarks>
        [HttpDelete("question")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteQuestionResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DeleteQuestionResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DeleteQuestionResponseDTO))]
        public async Task<IActionResult> DeleteQuestion(DeleteQuestionRequestDTO deleteRequest)
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
        /// <h2> **Description:** </h2>
        /// <h3> O character especificado responde o quiz informado. </h3>
        /// </remarks>
        [HttpPost("feedback")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveQuizFeedbackResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SaveQuizFeedbackResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SaveQuizFeedbackResponseDTO))]
        public async Task<IActionResult> PostQuizFeedback(SaveQuizFb_QuizFeedbackDTO quizFbReceived)
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
    }
}
