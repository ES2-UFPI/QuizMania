using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Services
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> GetQuizzesAsync();
        Task<Quiz> GetQuizAsync(long id);
        Task<QuizFeedback> SaveQuizAnswer(QuizFeedbackReceivedDTO quizFbReceived);
    }
}
