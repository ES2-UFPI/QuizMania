using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Controllers;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Services;
using QuizMania.WebAPI.DTOs.Output;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI.Tests.Controllers {
    [TestFixture]
    partial class CharacterControllerTests : DbContextTestBase {
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
    }
}