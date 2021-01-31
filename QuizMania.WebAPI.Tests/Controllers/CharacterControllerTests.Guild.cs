using System;
using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.Controllers;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Services;
using QuizMania.WebAPI.DTOs.Output;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;

namespace QuizMania.WebAPI.Tests.Controllers
{
    partial class CharacterControllerTests
    {
        private DatabaseContext GetGuildTestsDatabaseContext()
        {
            var context = GetUniqueDatabaseContext("GuildTestsDatabaseContext");
            context.Database.EnsureDeletedAsync().Wait();
            context.Database.EnsureCreatedAsync().Wait();

            var guild1 = new Guild() { Id = 1 };
            var guild2 = new Guild() { Id = 2 };
            context.Guilds.Add(guild1);
            context.Guilds.Add(guild2);

            context.Characters.Add(new Character { Id = 1 });
            context.Characters.Add(new Character { Id = 2, Guild = guild1 });

            context.SaveChanges();

            return context;
        }

        [TestCase(1, 999)]
        [TestCase(999, 1)]
        public async Task Test_Leave_JoinGuild_GuildOrCharacterNonExistent(long characterId, long guildId)
        {
            var context = GetGuildTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.Leave_JoinGuild(new Leave_JoinGuildRequestDTO() { CharacterId = characterId, GuildId = guildId});
            
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        public async Task Test_Leave_JoinGuild_ValidInput(long characterId, long guildId)
        {
            var context = GetGuildTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.Leave_JoinGuild(new Leave_JoinGuildRequestDTO() { CharacterId = characterId, GuildId = guildId });

            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var response = okResult.Value as Leave_JoinGuildResponseDTO;
            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            
            Assert.IsTrue(response.Result == Leave_JoinGuildResponseDTO.RequestResult.Success.ToString());
        }
    }
}
