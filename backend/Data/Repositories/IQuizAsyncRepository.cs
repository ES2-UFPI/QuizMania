using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public interface IQuizAsyncRepository
    {
        Task<IEnumerable<Quiz>> GetAllQuizzesAsync();
        Task<Quiz> GetQuizAsync(long id);
        Task<Question> GetQuestionAsync(long id);
        Task<Answer> GetAnswerAsync(long id);
        Task<IEnumerable<QuizFeedback>> GetQuizFbByCharAsync(long id);
        void AddQuiz(Quiz quiz);
        Task DeleteQuizAsync(Quiz quiz);
        Task DeleteQuestionAsync(Question question);
        Task SaveChangesAsync();
    }
}
