using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.DTOs;
using AutoMapper;
using System.Threading.Tasks;

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
