using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Services
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizReadDTO>> GetQuizzesAsync();
        Task<QuizReadDTO> GetQuizAsync(long id);
        Task<DeleteQuizRequestResultDTO> DeleteQuiz(DeleteQuizRequestDTO deleteRequest);
        Task<QuizReadDTO> SaveQuizAsync(QuizReceivedDTO quizReceived);
        Task<QuizFeedbackReadDTO> SaveQuizAnswerAsync(QuizFeedbackReceivedDTO quizFbReceived);
    }
}
