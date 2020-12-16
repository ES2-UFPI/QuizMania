using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.DTOs;
using AutoMapper;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;
using System.Linq;

namespace QuizMania.WebAPI.Controllers
{
    [Route("api/Quiz")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizAsyncRepository _repository;
        private readonly IMapper _mapper;

        public QuizController(IQuizAsyncRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizReadDTO>>> GetQuizzes()
        {
            var quizzes = await _repository.GetAllQuizzesAsync();
            return Ok(_mapper.Map<IEnumerable<QuizReadDTO>>(quizzes));
        }

        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizReadDTO>> GetQuiz(long id)
        {
            var quiz = await _repository.GetQuizAsync(id);
            return quiz != null ? Ok(_mapper.Map<QuizReadDTO>(quiz)) : NotFound();
        }

        //POST: api/Quiz/
        [HttpPost]
        public async Task<ActionResult<QuizFeedbackReadDTO>> PostQuizFeedback(QuizFeedbackReceivedDTO quizReceived)
        {
            int rightAnswerNumber = 0;
            Question question;

            QuizFeedback quizFeedback = _mapper.Map<QuizFeedback>(quizReceived);

            var quizFeedbackRead = new QuizFeedbackReadDTO()
            {
                QuizId = quizFeedback.QuizId
            };

            Quiz quiz = await _repository.GetQuizAsync(quizFeedback.QuizId);

            foreach (var questionAnswer in quizFeedback.QuestionAnswers)
            {
                question = (Question)quiz.Questions.Where(q => q.Id == questionAnswer.QuestionId).First();

                var correctAnswers = question.Choices.Where(c => c.IsCorrect).Select(c => c.Id).ToList();

                if (Enumerable.SequenceEqual(correctAnswers, questionAnswer.Answers.OrderBy(q=>q)))
                    rightAnswerNumber++;

                quizFeedbackRead.QuestionAnswers.Add(new QuestionAnswerDTO { QuestionId = question.Id, Answers = correctAnswers });
            }

            quizFeedbackRead.GoldGained = (rightAnswerNumber * 100) / quiz.Questions.Count;
            quizFeedbackRead.ExperienceGained = (rightAnswerNumber * 100) / quiz.Questions.Count;

            return Ok(quizFeedbackRead);
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
