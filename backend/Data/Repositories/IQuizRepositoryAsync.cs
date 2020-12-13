using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public interface IQuizAsyncRepository
    {
        Task<ActionResult<IEnumerable<Quiz>>> GetAllQuizzesAsync();
        Task<ActionResult<Quiz>> GetQuizAsync(long id);
    }
}
