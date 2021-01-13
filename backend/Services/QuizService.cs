using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs;

namespace QuizMania.WebAPI.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizAsyncRepository _quizRepo;
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public QuizService(IQuizAsyncRepository quizRepo, ICharacterService characterService, IMapper mapper)
        {
            _quizRepo = quizRepo;
            _characterService = characterService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuizReadDTO>> GetQuizzesAsync()
        {
            return _mapper.Map<IEnumerable<QuizReadDTO>>(await _quizRepo.GetAllQuizzesAsync());
        }

        public async Task<QuizReadDTO> GetQuizAsync(long id)
        {
            return _mapper.Map<QuizReadDTO>(await _quizRepo.GetQuizAsync(id));
        }

        public async Task<DeleteQuizRequestResultDTO> DeleteQuiz(DeleteQuizRequestDTO deleteRequest)
        {
            var result = new DeleteQuizRequestResultDTO
            {
                Request = deleteRequest
            };

            var quiz = await _quizRepo.GetQuizAsync(deleteRequest.QuizId);
            
            if (quiz == null)
            {
                result._result = DeleteQuizRequestResultDTO.RequestResult.QuizNotFound;
                return result;
            }

            if (quiz.Owner.Id != deleteRequest.CharacterId)
            {
                result._result = DeleteQuizRequestResultDTO.RequestResult.CharacterNotOwner;
                return result;
            }

            var deleted = await _quizRepo.DeleteQuizAsync(deleteRequest.QuizId);
            
            result._result = deleted ? DeleteQuizRequestResultDTO.RequestResult.Success : DeleteQuizRequestResultDTO.RequestResult.BadRequest;
            return result;
        }

        public async Task<QuizFeedbackReadDTO> SaveQuizAnswer(QuizFeedbackReceivedDTO quizFbReceived)
        {
            float rightAnswerNumber = 0;

            QuizFeedback quizFb = new QuizFeedback();

            quizFb.Quiz = await _quizRepo.GetQuizAsync(quizFbReceived.QuizId);
            
            if (quizFb.Quiz == null)
                return null;

            quizFb.Character = new Character() { Id = quizFbReceived.CharacterId };

            foreach (var qtAnswerReceived in quizFbReceived.QuestionAnswers)
            {
                var qtAnswer = new QuestionAnswer();

                qtAnswer.Question = await _quizRepo.GetQuestionAsync(qtAnswerReceived.QuestionId);
                
                if (qtAnswer.Question == null)
                    return null;

                foreach (var answerID in qtAnswerReceived.ChosenAnswerIds)
                {
                    var chonsenAnswer = await _quizRepo.GetAnswerAsync(answerID);
                   
                    if (chonsenAnswer == null)
                        return null;
                    
                    qtAnswer.ChosenAnswers.Add(chonsenAnswer);
                }

                quizFb.QuestionAnswers.Add(qtAnswer);

                var correctAnswerIds = qtAnswer.Question.Answers.Where(c => c.IsCorrect).Select(c => c.Id).ToHashSet();
                var AnswerIds = qtAnswerReceived.ChosenAnswerIds.ToHashSet();

                int hits = Enumerable.Intersect(AnswerIds, correctAnswerIds).Count();
                int misses = AnswerIds.Except(correctAnswerIds).Count();

                if (misses <= hits)
                {
                    rightAnswerNumber += (float) (hits - misses) / correctAnswerIds.Count();
                }
            }

            quizFb.PercentageOfCorrectChosenAnswers = (float) Math.Round(rightAnswerNumber * 100 / quizFb.Quiz.Questions.Count, 2);

            //Save awnsers
            if ( !(await _characterService.SaveQuizfeedback(quizFb)) )
                return null;
            
            //fill with correct answers
            foreach (var qtAnswer in quizFb.QuestionAnswers)
            {
                qtAnswer.ChosenAnswers = qtAnswer.Question.Answers.Where(c => c.IsCorrect).ToList();
            }

            //Return correct answers
            return _mapper.Map<QuizFeedbackReadDTO>(quizFb);
        }
    }
}
