using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Controllers;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Services;
using QuizMania.WebAPI.DTOs.Output;
using QuizMania.WebAPI.Models;
using System.Collections.Generic;
using QuizMania.WebAPI.DTOs.Input;

namespace QuizMania.WebAPI.Tests.Controllers
{
    [TestFixture]
    partial class QuizControllerTests : DbContextTestBase
    {
        private DatabaseContext GetQuizTestsDatabaseContext()
        {
            var context = GetUniqueDatabaseContext("QuizTestsDatabaseContext");
            context.Database.EnsureDeletedAsync().Wait();
            context.Database.EnsureCreatedAsync().Wait();

            var char1 = new Character();
            var char2 = new Character();

            var quiz1 = new Quiz()
            {
                Owner = char1,
            };

            var quiz2 = new Quiz()
            {
                Owner = char1,
            };

            var question1 = new Question()
            {
                Answers = new List<Answer>()
                {
                    new Answer { IsCorrect = false },

                    new Answer { IsCorrect = false },

                    new Answer { IsCorrect = true },

                    new Answer { IsCorrect = false }
                }
            };

            var question2 = new Question()
            {
                Answers = new List<Answer>()
                {
                    new Answer { IsCorrect = false },

                    new Answer { IsCorrect = true }
                }
            };

            var question3 = new Question()
            {
                Answers = new List<Answer>()
                {
                    new Answer { IsCorrect = true },

                    new Answer { IsCorrect = true }
                }
            };

            context.Characters.Add(char1);
            context.Characters.Add(char2);

            quiz1.Questions.Add(question1);

            quiz2.Questions.Add(question2);
            quiz2.Questions.Add(question3);

            context.Quizzes.Add(quiz1);
            context.Quizzes.Add(quiz2);

            context.SaveChanges();

            return context;
        }
          
        [Test]
        public async Task Test_GetQuizzes_Success()
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));
            var quizCount = await context.Quizzes.CountAsync();

            var actionResult = await controller.GetQuizzes();

            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var quizzesReadDTO = okResult.Value as ICollection<QuizReadDTO>;
            Assert.NotNull(quizzesReadDTO);

            Assert.AreEqual(quizCount, quizzesReadDTO.Count);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public async Task Test_GetQuiz_InvalidId(long value)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var actionResult = await controller.GetQuiz(value);

            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }

        [TestCase(999)]
        public async Task Test_GetQuiz_NotFound(long value)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var actionResult = await controller.GetQuiz(value);

            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task Test_GetQuiz_Success(long value)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));
            var actionResult = await controller.GetQuiz(value);

            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var quizRead = okResult.Value as QuizReadDTO;
            Assert.NotNull(quizRead);

            Assert.IsTrue(quizRead.Id == value);
        }

        [TestCase(1)]
        public async Task Test_PostQuiz_Success(long ownerId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var quizDTO = new SaveQuiz_QuizDTO
            {
                Title = "Teste",
                OwnerId = ownerId,
            };

            var question1DTO = new SaveQuiz_QuestionDTO
            {
                Text = "Test Question",
                Answers = new List<SaveQuiz_AnswerDTO>()
                {
                    new SaveQuiz_AnswerDTO { Text = "Empty", IsCorrect = true },

                    new SaveQuiz_AnswerDTO { Text = "Empty", IsCorrect = true }
                }
            };

            var question2DTO = new SaveQuiz_QuestionDTO
            {
                Text = "Test Question",
                Answers = new List<SaveQuiz_AnswerDTO>()
                {
                    new SaveQuiz_AnswerDTO { Text = "Empty", IsCorrect = false },

                    new SaveQuiz_AnswerDTO { Text = "Empty", IsCorrect = true }
                }
            };

            quizDTO.Questions.Add(question1DTO);
            quizDTO.Questions.Add(question2DTO);

            var quizCountBefore = await context.Quizzes.CountAsync();
            var questionCountBefore = await context.Questions.CountAsync();
            var answerCountBefore = await context.Answers.CountAsync();

            var actionResult = await controller.PostQuiz(quizDTO);

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var quizCountAfter = await context.Quizzes.CountAsync();
            var questionCountAfter = await context.Questions.CountAsync();
            var answerCountAfter = await context.Answers.CountAsync();

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as SaveQuizResponseDTO;

            Assert.NotNull(response);
            Assert.AreEqual(SaveQuizResponseDTO.RequestResult.Success.ToString(), response.Result);

            Assert.AreEqual(quizCountBefore + 1, quizCountAfter);
            Assert.AreEqual(questionCountBefore + 2, questionCountAfter);
            Assert.AreEqual(answerCountBefore + 4, answerCountAfter);
        }

        [TestCase(999)]
        public async Task Test_PostQuiz_OwnerNotFound(long ownerId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var actionResult = await controller.PostQuiz(new SaveQuiz_QuizDTO { Title = "Teste", OwnerId = ownerId });

            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }

        [TestCase(1, 2)]
        public async Task Test_DeleteQuiz_Success(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            
            var quizCountBefore = await context.Quizzes.CountAsync();
            var questionCountBefore = await context.Questions.CountAsync();
            var answerCountBefore = await context.Answers.CountAsync();

            var actionResult = await controller.DeleteQuiz(new DeleteQuizRequestDTO { CharacterId = characterId, QuizId = quizId});

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var quizCountAfter = await context.Quizzes.CountAsync();
            var questionCountAfter = await context.Questions.CountAsync();
            var answerCountAfter = await context.Answers.CountAsync();

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as DeleteQuizResponseDTO;
            
            Assert.NotNull(response);
            Assert.AreEqual(DeleteQuizResponseDTO.RequestResult.Success.ToString(), response.Result);

            Assert.AreEqual(quizCountBefore - 1, quizCountAfter);
            Assert.AreEqual(questionCountBefore - 2, questionCountAfter);
            Assert.AreEqual(answerCountBefore - 4, answerCountAfter);
        }

        [TestCase(1, 999)]
        public async Task Test_DeleteQuiz_QuizNotFound(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var actionResult = await controller.DeleteQuiz(new DeleteQuizRequestDTO { CharacterId = characterId, QuizId = quizId });

            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }

        [TestCase(2, 1)]
        public async Task Test_DeleteQuiz_CharacterNotOwner(long characterId, long quizId)
        {
            var context = GetQuizTestsDatabaseContext();
            var controller = new QuizController(new QuizService(new QuizRepository(context), new CharacterRepository(context),
                             new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper), Mapper));

            var actionResult = await controller.DeleteQuiz(new DeleteQuizRequestDTO { CharacterId = characterId, QuizId = quizId });

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }
    }
}
