using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Controllers;
using QuizMania.WebAPI.Services;
using QuizMania.WebAPI.DTOs.Output;
using System.Collections.Generic;
using QuizMania.WebAPI.DTOs.Input;

namespace QuizMania.WebAPI.Tests.Controllers
{
    partial class QuizControllerTests
    {
        [TestCase(1, 1)]
        public async Task Test_PostQuestion_Success(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var questionDTO = new SaveQuestion_QuestionDTO
            {
                QuizId = characterId,
                CharacterId = quizId,
                Text = "Test Question",
                Answers = new List<SaveQuestion_AnswerDTO>()
                {
                    new SaveQuestion_AnswerDTO { Text = "Empty", IsCorrect = false },

                    new SaveQuestion_AnswerDTO { Text = "Empty", IsCorrect = true }
                }
            };

            var questionCountBefore = await context.Questions.CountAsync();
            var answerCountBefore = await context.Answers.CountAsync();

            var actionResult = await controller.PostQuestion(questionDTO);

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var questionCountAfter = await context.Questions.CountAsync();
            var answerCountAfter = await context.Answers.CountAsync();

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as SaveQuestionResponseDTO;

            Assert.NotNull(response);
            Assert.AreEqual(SaveQuestionResponseDTO.RequestResult.Success.ToString(), response.Result);

            Assert.AreEqual(questionCountBefore + 1, questionCountAfter);
            Assert.AreEqual(answerCountBefore + 2, answerCountAfter);
        }

        [TestCase(1, 999)]
        public async Task Test_PostQuestion_QuizNotFound(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var actionResult = await controller.PostQuestion(new SaveQuestion_QuestionDTO { CharacterId = characterId, QuizId = quizId });

            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }

        [TestCase(1, 1)]
        public async Task Test_PostQuestion_QuestionWithoutCorrectAnswer(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var questionDTO = new SaveQuestion_QuestionDTO
            {
                QuizId = characterId,
                CharacterId = quizId,
                Text = "Test Question",
                Answers = new List<SaveQuestion_AnswerDTO>()
                {
                    new SaveQuestion_AnswerDTO { Text = "Empty", IsCorrect = false },

                    new SaveQuestion_AnswerDTO { Text = "Empty", IsCorrect = false }
                }
            };

            var actionResult = await controller.PostQuestion(questionDTO);

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }

        [TestCase(2, 1)]
        public async Task Test_PostQuestion_CharacterNotOwner(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var actionResult = await controller.PostQuestion(new SaveQuestion_QuestionDTO { CharacterId = characterId, QuizId = quizId });

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }

        [TestCase(1, 1, 1)]
        public async Task Test_DeleteQuestion_Success(long characterId, long quizId, long questionId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var questionCountBefore = await context.Questions.CountAsync();
            var answerCountBefore = await context.Answers.CountAsync();

            var actionResult = await controller.DeleteQuestion(new DeleteQuestionRequestDTO
            {
                CharacterId = characterId,
                QuizId = quizId,
                QuestionId = questionId
            });

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var questionCountAfter = await context.Questions.CountAsync();
            var answerCountAfter = await context.Answers.CountAsync();

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as DeleteQuestionResponseDTO;

            Assert.NotNull(response);
            Assert.AreEqual(DeleteQuestionResponseDTO.RequestResult.Success.ToString(), response.Result);

            Assert.AreEqual(questionCountBefore - 1, questionCountAfter);
            Assert.AreEqual(answerCountBefore - 4, answerCountAfter);
        }

        [TestCase(1, 1, 999)]
        public async Task Test_DeleteQuestion_QuestionNotFound(long characterId, long quizId, long questionId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var actionResult = await controller.DeleteQuestion(new DeleteQuestionRequestDTO
            {
                CharacterId = characterId,
                QuizId = quizId,
                QuestionId = questionId
            });

            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }
    }
}
