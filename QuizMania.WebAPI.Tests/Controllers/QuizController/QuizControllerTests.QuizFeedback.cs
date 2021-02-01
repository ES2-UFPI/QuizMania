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
    [TestFixture]
    partial class QuizControllerTests
    {
        [TestCase(1, 2)]
        public async Task Test_PostQuizFeedback_Success(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var quizFbDTO = new SaveQuizFb_QuizFeedbackDTO
            {
                CharacterId = characterId,
                QuizId = quizId
            };

            var questionAnswer1DTO = new SaveQuizFb_QuestionAnswerDTO
            {
                QuestionId = 2,
                ChosenAnswerIds = new List<long> { 6 }
            };

            var questionAnswer2DTO = new SaveQuizFb_QuestionAnswerDTO
            {
                QuestionId = 3,
                ChosenAnswerIds = new List<long> { 7, 8 }
            };

            quizFbDTO.QuestionAnswers.Add(questionAnswer1DTO);
            quizFbDTO.QuestionAnswers.Add(questionAnswer2DTO);

            var quizFbCountBefore = await context.QuizFeedbacks.CountAsync();
            var questionAnswerCountBefore = await context.QuestionAnswers.CountAsync();
            var charBefore = await context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
            var charGoldBefore = charBefore.Gold;
            var charXPBefore = charBefore.TotalXP;
            var charLevelBefore = charBefore.Level;

            var actionResult = await controller.PostQuizFeedback(quizFbDTO);

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var quizFbCountAfter = await context.QuizFeedbacks.CountAsync();
            var questionAnswerCountAfter = await context.QuestionAnswers.CountAsync();
            var charAfter = await context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
            var charGoldAfter = charAfter.Gold;
            var charXPAfter = charAfter.TotalXP;
            var charLevelAfter = charAfter.Level;

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as SaveQuizFeedbackResponseDTO;

            Assert.NotNull(response);
            Assert.AreEqual(SaveQuizFeedbackResponseDTO.RequestResult.Success.ToString(), response.Result);

            var quizFbResult = response.QuizFeedback;

            Assert.AreEqual(quizFbCountBefore + 1, quizFbCountAfter);
            Assert.AreEqual(questionAnswerCountBefore + 2, questionAnswerCountAfter);
            Assert.AreEqual(charGoldAfter - charGoldBefore, quizFbResult.GoldGained);
            Assert.AreEqual(charXPAfter - charXPBefore, quizFbResult.ExperienceGained);
            Assert.AreEqual(100, quizFbResult.GoldGained);
            Assert.AreEqual(100, quizFbResult.ExperienceGained);
            Assert.AreEqual(charLevelAfter - charLevelBefore, quizFbResult.LevelGained);
        }

        [TestCase(1, 2)]
        public async Task Test_PostQuizFeedback_SameQuizTwice(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var quizFbDTO = new SaveQuizFb_QuizFeedbackDTO
            {
                CharacterId = characterId,
                QuizId = quizId
            };

            var questionAnswer1DTO = new SaveQuizFb_QuestionAnswerDTO
            {
                QuestionId = 2,
                ChosenAnswerIds = new List<long> { 6 }
            };

            var questionAnswer2DTO = new SaveQuizFb_QuestionAnswerDTO
            {
                QuestionId = 3,
                ChosenAnswerIds = new List<long> { 7, 8 }
            };

            quizFbDTO.QuestionAnswers.Add(questionAnswer1DTO);
            quizFbDTO.QuestionAnswers.Add(questionAnswer2DTO);

            var actionResult = await controller.PostQuizFeedback(quizFbDTO);

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            actionResult = await controller.PostQuizFeedback(quizFbDTO);

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as SaveQuizFeedbackResponseDTO;

            Assert.NotNull(response);
            Assert.AreEqual(SaveQuizFeedbackResponseDTO.RequestResult.Success.ToString(), response.Result);

            var quizFbResult = response.QuizFeedback;

            Assert.AreEqual(0, quizFbResult.GoldGained);
            Assert.AreEqual(0, quizFbResult.ExperienceGained);
            Assert.AreEqual(0, quizFbResult.LevelGained);
        }

        [TestCase(1, 2)]
        public async Task Test_PostQuizFeedback_AnswerNotFound(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var quizFbDTO = new SaveQuizFb_QuizFeedbackDTO
            {
                CharacterId = characterId,
                QuizId = quizId
            };

            var questionAnswer1DTO = new SaveQuizFb_QuestionAnswerDTO
            {
                QuestionId = 2,
                ChosenAnswerIds = new List<long> { 6 }
            };

            var questionAnswer2DTO = new SaveQuizFb_QuestionAnswerDTO
            {
                QuestionId = 3,
                ChosenAnswerIds = new List<long> { 7, 999 }
            };

            quizFbDTO.QuestionAnswers.Add(questionAnswer1DTO);
            quizFbDTO.QuestionAnswers.Add(questionAnswer2DTO);

            var actionResult = await controller.PostQuizFeedback(quizFbDTO);

            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }

        [TestCase(1, 2)]
        public async Task Test_PostQuizFeedback_InvalidQuizFeedback(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var quizFbDTO = new SaveQuizFb_QuizFeedbackDTO
            {
                CharacterId = characterId,
                QuizId = quizId
            };

            var questionAnswer1DTO = new SaveQuizFb_QuestionAnswerDTO
            {
                QuestionId = 2,
                ChosenAnswerIds = new List<long> { 6 }
            };

            var questionAnswer2DTO = new SaveQuizFb_QuestionAnswerDTO
            {
                QuestionId = 3,
                ChosenAnswerIds = new List<long>()
            };

            quizFbDTO.QuestionAnswers.Add(questionAnswer1DTO);
            quizFbDTO.QuestionAnswers.Add(questionAnswer2DTO);

            var actionResult = await controller.PostQuizFeedback(quizFbDTO);

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }
    }
}
