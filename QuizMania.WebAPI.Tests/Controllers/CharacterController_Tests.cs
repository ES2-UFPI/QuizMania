using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.Controllers;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Services;
using QuizMania.WebAPI.DTOs.Output;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI.Tests.Controllers {
    [TestFixture]
    class CharacterControllerTests : DbContextTestBase {
        [TestCase(-1)]
        [TestCase(0)]
        public async Task Test_GetCharacter_InvalidId(long value) {
            var controller   = new CharacterController(new CharacterService(new CharacterRepository(DbContext), new ItemRepository(DbContext), Mapper));
            var actionResult = await controller.GetCharacter(value);

            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }

        [TestCase(999)]
        public async Task Test_GetCharacter_NonExistent(long value) {
            var controller   = new CharacterController(new CharacterService(new CharacterRepository(DbContext), new ItemRepository(DbContext), Mapper));
            var actionResult = await controller.GetCharacter(value);

            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }

        [TestCase(1)]
        public async Task Test_GetCharacter_Existent(long value) {
            var controller   = new CharacterController(new CharacterService(new CharacterRepository(DbContext), new ItemRepository(DbContext), Mapper));
            var actionResult = await controller.GetCharacter(value);

            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var characterInfo = okResult.Value as CharacterInfoDTO;
            Assert.NotNull(characterInfo);

            Assert.IsTrue(characterInfo.Id == value);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task Test_GetRanking_InvalidPageSize(int pageSize) {
            var controller   = new CharacterController(new CharacterService(new CharacterRepository(DbContext), new ItemRepository(DbContext), Mapper));
            var actionResult = await controller.GetRanking(pageSize);

            Assert.IsInstanceOf<BadRequestResult>(actionResult);
        }

        [TestCase(1)]
        public async Task Test_GetRanking_CountWithPageSizeBelowCharacterCount(int pageSize) {
            Assert.LessOrEqual(pageSize, CharacterService.MaxRankingPageSize);

            var charactersCount = DbContext.Characters.Count();

            Assert.Greater(charactersCount, pageSize);

            var controller   = new CharacterController(new CharacterService(new CharacterRepository(DbContext), new ItemRepository(DbContext), Mapper));
            var actionResult = await controller.GetRanking(pageSize);

            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var ranking = okResult.Value as CharacterRankingDTO;
            Assert.NotNull(ranking);

            Assert.IsTrue(ranking.Ranking.Count == pageSize);

            Assert.Less(ranking.Ranking.Count, charactersCount);
        }

        [TestCase(CharacterService.MaxRankingPageSize)]
        public async Task Test_GetRanking_CountWithPageSizeAboveCharacterCount(int pageSize) {
            var charactersCount = DbContext.Characters.Count();

            Assert.GreaterOrEqual(pageSize, charactersCount);

            var controller   = new CharacterController(new CharacterService(new CharacterRepository(DbContext), new ItemRepository(DbContext), Mapper));
            var actionResult = await controller.GetRanking(pageSize);

            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var ranking = okResult.Value as CharacterRankingDTO;
            Assert.NotNull(ranking);

            Assert.IsTrue(ranking.Ranking.Count == charactersCount);

            Assert.LessOrEqual(ranking.Ranking.Count, pageSize);
        }

        [TestCase(CharacterService.MaxRankingPageSize + 1)]
        [TestCase(CharacterService.MaxRankingPageSize + CharacterService.MaxRankingPageSize + 1)]
        public async Task Test_GetRanking_CountWithPageSizeAboveMax(int pageSize) {
            Assert.Greater(pageSize, CharacterService.MaxRankingPageSize);

            var charactersCount = DbContext.Characters.Count();

            Assert.Greater(charactersCount, CharacterService.MaxRankingPageSize);

            var controller   = new CharacterController(new CharacterService(new CharacterRepository(DbContext), new ItemRepository(DbContext), Mapper));
            var actionResult = await controller.GetRanking(pageSize);

            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var ranking = okResult.Value as CharacterRankingDTO;
            Assert.NotNull(ranking);

            Assert.IsTrue(ranking.Ranking.Count == CharacterService.MaxRankingPageSize);

            Assert.Less(ranking.Ranking.Count, charactersCount);
        }

        [TestCase(CharacterService.MaxRankingPageSize)]
        public async Task Test_GetRanking_Ordering(int pageSize) {
            var context = new DatabaseContext(DbContextBuilder.Options);

            var bestCharacter = new Character {
                Gold         = 10,
                Name         = "TheBest",
                HealthPoints = 10,
                TotalXP      = 999999,
            };

            var secondBestCharacter = new Character {
                Gold         = 10,
                Name         = "TheSecondBest",
                HealthPoints = 10,
                TotalXP      = 888888,
            };

            var worstCharacter = new Character {
                Gold         = 10,
                Name         = "TheWorst",
                HealthPoints = 10,
                TotalXP      = 0,
            };

            await context.Characters.AddAsync(secondBestCharacter);
            await context.Characters.AddAsync(worstCharacter);
            await context.Characters.AddAsync(bestCharacter);

            await context.SaveChangesAsync();

            var charactersCount = context.Characters.Count();

            var controller   = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));
            var actionResult = await controller.GetRanking(pageSize);

            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var rankingDto = okResult.Value as CharacterRankingDTO;
            Assert.NotNull(rankingDto);

            var ranking = rankingDto.Ranking.ToArray();

            Assert.IsTrue(ranking.Length == charactersCount);

            Assert.Less(ranking[0].Name, bestCharacter.Name);
            Assert.Less(ranking[1].Name, secondBestCharacter.Name);
            Assert.Less(ranking[2].Name, worstCharacter.Name);
        }
    }
}