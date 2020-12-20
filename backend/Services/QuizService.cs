using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizAsyncRepository _repository;

        public QuizService(IQuizAsyncRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesAsync()
        {
            return await _repository.GetAllQuizzesAsync();
        }

        public async Task<Quiz> GetQuizAsync(long id)
        {
            return await _repository.GetQuizAsync(id);
        }

        public async Task<QuizFeedback> SaveQuizAnswer(QuizFeedbackReceivedDTO quizFbReceived)
        {
            int rightAnswerNumber = 0;

            QuizFeedback quizFb = new QuizFeedback();
            QuizFeedback quizReadFb = new QuizFeedback();

            Quiz quiz = await _repository.GetQuizAsync(quizFbReceived.QuizId);
            
            if (quiz == null)
                return null;

            foreach (var qtAnswerReceived in quizFbReceived.QuestionAnswers)
            {
                var qtAnswer = new QuestionAnswer();

                qtAnswer.Question = await _repository.GetQuestionAsync(qtAnswerReceived.QuestionId);
                

                if (qtAnswer.Question == null)
                    return null;

                foreach (var answerID in qtAnswerReceived.AnswerIds)
                {
                    var choice = await _repository.GetChoiceAsync(answerID);
                   
                    if (choice == null)
                        return null;
                    
                    qtAnswer.Answers.Add(choice);
                }

                quizFb.QuestionAnswers.Add(qtAnswer);

                var correctAnswers = qtAnswer.Question.Choices.Where(c => c.IsCorrect).Select(c => c.Id).ToHashSet();

                if (Enumerable.SequenceEqual(correctAnswers, qtAnswerReceived.AnswerIds.OrderBy(q => q)))
                    rightAnswerNumber++;
            }

            quizFb.Quiz = quiz;
            quizFb.GoldGained = (rightAnswerNumber * 100) / quiz.Questions.Count;
            quizFb.ExperienceGained = (rightAnswerNumber * 100) / quiz.Questions.Count;

            //Save awnsers
            /*_repository.SaveQuizFeedback(quizFb);
            await _repository.SaveChangesAsync();*/
            
            foreach(var qtAnswer in quizFb.QuestionAnswers)
            {
                qtAnswer.Answers = qtAnswer.Question.Choices.Where(c => c.IsCorrect).ToList();
            }

            //Return right answers
            return quizFb;
        }
    }
}
