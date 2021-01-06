using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Services
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizReadDTO>> GetQuizzesAsync();
        Task<QuizReadDTO> GetQuizAsync(long id);
        Task<QuizFeedbackReadDTO> SaveQuizAnswer(QuizFeedbackReceivedDTO quizFbReceived);
    }
}
