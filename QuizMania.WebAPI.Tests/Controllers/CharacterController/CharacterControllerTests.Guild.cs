using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.Controllers;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Services;
using QuizMania.WebAPI.DTOs.Output;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        [Test]
        public async Task Test_GetGuilds_Success()
        {
            var context = GetGuildTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var guildCount = await context.Guilds.CountAsync();

            var actionResult = await controller.GetGuilds();
            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okResult = actionResult as OkObjectResult;
            var guildsInfoDTO = okResult.Value as ICollection<GuildInfoDTO>;

            Assert.NotNull(guildsInfoDTO);

            Assert.AreEqual(guildCount, guildsInfoDTO.Count);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        public async Task Test_Leave_JoinGuild_Success(long characterId, long guildId)
        {
            var context = GetGuildTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.Leave_JoinGuild(new Leave_JoinGuildRequestDTO { CharacterId = characterId, GuildId = guildId });
            
            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as Leave_JoinGuildResponseDTO;
            
            Assert.NotNull(response);
            Assert.AreEqual(Leave_JoinGuildResponseDTO.RequestResult.Success.ToString(), response.Result);
        }

        [TestCase(1, 999)]
        [TestCase(999, 1)]
        public async Task Test_Leave_JoinGuild_GuildNotFoundOrCharacterNotFound(long characterId, long guildId)
        {
            var context = GetGuildTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.Leave_JoinGuild(new Leave_JoinGuildRequestDTO { CharacterId = characterId, GuildId = guildId });

            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }
    }
}
