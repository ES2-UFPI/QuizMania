using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizAsyncRepository _quizRepo;
        private readonly ICharacterAsyncRepository _characterRepo;
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public QuizService(IQuizAsyncRepository quizRepo, ICharacterAsyncRepository characterRepo, ICharacterService characterService, IMapper mapper)
        {
            _quizRepo = quizRepo;
            _characterRepo = characterRepo;
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

        public async Task<SaveQuizResponseDTO> SaveQuizAsync(SaveQuiz_QuizDTO quizReceived)
        {
            var result = new SaveQuizResponseDTO();

            var quiz = _mapper.Map<Quiz>(quizReceived);

            quiz.Owner = await _characterRepo.GetCharacterAsync(quizReceived.OwnerId);

            if(quiz.Owner == null)
            {
                result._result = SaveQuizResponseDTO.RequestResult.OwnerNotFound;
                return result;
            }

            if(quiz.Questions.Count == 0)
            {
                result._result = SaveQuizResponseDTO.RequestResult.EmptyAtribute;
                return result;
            }

            foreach (var question in quiz.Questions)
            {
                if (question.Answers.Count == 0)
                {
                    result._result = SaveQuizResponseDTO.RequestResult.EmptyAtribute;
                    return result;
                }

                if (!ValidateQuestion(question))
                {
                    result._result = SaveQuizResponseDTO.RequestResult.QuestionWithoutCorrectAnswer;
                    return result;
                }
            }

            _quizRepo.AddQuiz(quiz);

            try
            {
                await _quizRepo.SaveChangesAsync();
                result._result = SaveQuizResponseDTO.RequestResult.Success;
                result.Quiz = _mapper.Map<QuizReadDTO>(quiz);
            }
            catch (Exception)
            {
                result._result = SaveQuizResponseDTO.RequestResult.BadRequest;
            }

            return result;
        }

        public async Task<DeleteQuizResponseDTO> DeleteQuizAsync(DeleteQuizRequestDTO deleteRequest)
        {
            var result = new DeleteQuizResponseDTO
            {
                Request = deleteRequest
            };

            var quiz = await _quizRepo.GetQuizAsync(deleteRequest.QuizId);
            
            if (quiz == null)
            {
                result._result = DeleteQuizResponseDTO.RequestResult.QuizNotFound;
                return result;
            }

            if (quiz.Owner.Id != deleteRequest.CharacterId)
            {
                result._result = DeleteQuizResponseDTO.RequestResult.CharacterNotOwner;
                return result;
            }

            _quizRepo.DeleteQuiz(quiz);

            try
            {
                await _quizRepo.SaveChangesAsync();
                result._result = DeleteQuizResponseDTO.RequestResult.Success;

            }catch(Exception)
            {
                result._result = DeleteQuizResponseDTO.RequestResult.BadRequest;
            }

            return result;
        }

        public async Task<SaveQuizFeedbackResponseDTO> SaveQuizFeedbackAsync(SaveQuizFb_QuizFeedbackDTO quizFbReceived)
        {
            float rightAnswerNumber = 0;

            var result = new SaveQuizFeedbackResponseDTO();

            QuizFeedback quizFb = new QuizFeedback();

            quizFb.Quiz = await _quizRepo.GetQuizAsync(quizFbReceived.QuizId);
            
            if (quizFb.Quiz == null)
            {
                result._result = SaveQuizFeedbackResponseDTO.RequestResult.QuizNotFound;
                return result;
            }

            if(quizFb.Quiz.Questions.Count != quizFbReceived.QuestionAnswers.Count)
            {
                result._result = SaveQuizFeedbackResponseDTO.RequestResult.InvalidQuizFeedback;
                return result;
            }
                
            quizFb.Character = new Character() { Id = quizFbReceived.CharacterId };

            foreach (var qtAnswerReceived in quizFbReceived.QuestionAnswers)
            {
                var qtAnswer = new QuestionAnswer();

                qtAnswer.Question = quizFb.Quiz.Questions.Where(q => q.Id == qtAnswerReceived.QuestionId).FirstOrDefault();
                
                if (qtAnswer.Question == null)
                {
                    result._result = SaveQuizFeedbackResponseDTO.RequestResult.QuestionNotFound;
                    return result;
                } 
                
                if(qtAnswerReceived.ChosenAnswerIds.Count == 0)
                {
                    result._result = SaveQuizFeedbackResponseDTO.RequestResult.InvalidQuizFeedback;
                    return result;
                }

                foreach (var answerID in qtAnswerReceived.ChosenAnswerIds)
                {
                    var chonsenAnswer = qtAnswer.Question.Answers.Where(a => a.Id == answerID).FirstOrDefault();
                   
                    if (chonsenAnswer == null)
                    {
                        result._result = SaveQuizFeedbackResponseDTO.RequestResult.AnswerNotFound;
                        return result;
                    }
                        
                    qtAnswer.ChosenAnswers.Add(chonsenAnswer);
                }

                quizFb.QuestionAnswers.Add(qtAnswer);

                var correctAnswerIds = qtAnswer.Question.Answers.Where(c => c.IsCorrect).Select(c => c.Id).ToHashSet();
                var AnswerIds = qtAnswerReceived.ChosenAnswerIds.ToHashSet();

                int hits = Enumerable.Intersect(AnswerIds, correctAnswerIds).Count();
                int misses = AnswerIds.Except(correctAnswerIds).Count();

                if (misses <= hits)
                {
                    rightAnswerNumber += (float) (hits - misses) / correctAnswerIds.Count;
                }
            }

            quizFb.PercentageOfCorrectChosenAnswers = (float) Math.Round(rightAnswerNumber * 100 / quizFb.Quiz.Questions.Count, 2);

            //Save awnsers
            result._result = await _characterService.SaveQuizfeedback(quizFb);
            
            if(result._result != SaveQuizFeedbackResponseDTO.RequestResult.Success)
                return result;
            
            //fill with correct answers
            foreach (var qtAnswer in quizFb.QuestionAnswers)
            {
                qtAnswer.ChosenAnswers = qtAnswer.Question.Answers.Where(c => c.IsCorrect).ToList();
            }

            //Return correct answers
            result.QuizFeedback = _mapper.Map<QuizFeedbackReadDTO>(quizFb);
            return result;
        }

        private static bool ValidateQuestion(Question question)
        {
            foreach (var answer in question.Answers)
            {
                if (answer.IsCorrect)
                    return true;
            }

            return false;
        }
    }
}
