using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.Controllers;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Services;
using QuizMania.WebAPI.DTOs.Output;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizMania.WebAPI.Tests.Controllers
{
    partial class CharacterControllerTests
    {
        private DatabaseContext GetGoldTestsDatabaseContext()
        {
            var context = GetUniqueDatabaseContext("GoldTestsDatabaseContext");
            context.Database.EnsureDeletedAsync().Wait();
            context.Database.EnsureCreatedAsync().Wait();

            var char1 = new Character { Gold = 3000 };

            context.Characters.Add(char1);

            context.SaveChanges();

            return context;
        }

        [TestCase(1, 200)]
        public async Task Test_TryExpendGold_Success(long characterId, int expenseRequested)
        {
            var context = GetGoldTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var charGoldBefore = (await context.Characters.FirstOrDefaultAsync(c => c.Id == characterId)).Gold;
             
            var actionResult = await controller.TryExpendGold(new GoldExpenseRequestDTO { CharacterId = characterId, ExpenseRequested = expenseRequested });

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var charGoldAfter = (await context.Characters.FirstOrDefaultAsync(c => c.Id == characterId)).Gold;

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as GoldExpenseResponseDTO;

            Assert.NotNull(response);
            Assert.AreEqual(GoldExpenseResult.Authorized.ToString(), response.ResultMessage);

            Assert.AreEqual(charGoldBefore - charGoldAfter, expenseRequested);
        }

        [TestCase(1, 9999)]
        public async Task Test_TryExpendGold_NotEnoughResources(long characterId, int expenseRequested)
        {
            var context = GetGoldTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.TryExpendGold(new GoldExpenseRequestDTO { CharacterId = characterId, ExpenseRequested = expenseRequested });

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }
    }
}
