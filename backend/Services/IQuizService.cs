using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Services
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizReadDTO>> GetQuizzesAsync();
        Task<QuizReadDTO> GetQuizAsync(long id);
        Task<SaveQuizResponseDTO> SaveQuizAsync(SaveQuiz_QuizDTO quizReceived);
        Task<DeleteQuizResponseDTO> DeleteQuizAsync(DeleteQuizRequestDTO deleteRequest);
        Task<SaveQuestionResponseDTO> SaveQuestionAsync(SaveQuestion_QuestionDTO questionReceived);
        Task<SaveQuizFeedbackResponseDTO> SaveQuizFeedbackAsync(SaveQuizFb_QuizFeedbackDTO quizFbReceived);
    }
}
