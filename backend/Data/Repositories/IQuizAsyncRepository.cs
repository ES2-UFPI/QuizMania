using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public interface IQuizAsyncRepository
    {
        Task<IEnumerable<Quiz>> GetAllQuizzesAsync();
        Task<Quiz> GetQuizAsync(long id);
        public Task<Question> GetQuestionAsync(long id);
        public Task<Answer> GetAnswerAsync(long id);
        void SaveQuizFeedback(QuizFeedback quizFeedback);
        Task<int> SaveChangesAsync();
    }
}
