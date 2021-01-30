﻿using System;
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
    partial class CharacterControllerTests {
        private DatabaseContext GetRankingTestsDatabaseContext() {
            var context = GetUniqueDatabaseContext("RankingTestsDatabaseContext");
            
            context.Characters.Add(new Character {TotalXP = 5, GuildId = 0});
            context.Characters.Add(new Character {TotalXP = 8, GuildId = 0});
            context.Characters.Add(new Character {TotalXP = 2, GuildId = 0});
            context.Characters.Add(new Character {TotalXP = 4, GuildId = 0});

            context.Characters.Add(new Character {TotalXP = 5, GuildId = 1});
            context.Characters.Add(new Character {TotalXP = 7, GuildId = 1});
            context.Characters.Add(new Character {TotalXP = 9, GuildId = 1});
            context.Characters.Add(new Character {TotalXP = 3, GuildId = 1});
            context.Characters.Add(new Character {TotalXP = 7, GuildId = 1});
            context.Characters.Add(new Character {TotalXP = 5, GuildId = 1});

            context.Characters.Add(new Character {TotalXP = 1, GuildId = 2});
            context.Characters.Add(new Character {TotalXP = 6, GuildId = 2});
            context.Characters.Add(new Character {TotalXP = 8, GuildId = 2});
            context.Characters.Add(new Character {TotalXP = 2, GuildId = 2});
            context.Characters.Add(new Character {TotalXP = 6, GuildId = 2});

            context.SaveChanges();

            return context;
        }
        
        [TestCase(-2)]
        public async Task Test_GetRanking_InvalidGuildId(int guildId) {
            var controller   = new CharacterController(new CharacterService(new CharacterRepository(DbContext), new ItemRepository(DbContext), Mapper));
            var actionResult = await controller.GetRanking(guildId);

            Assert.IsInstanceOf<BadRequestResult>(actionResult);
        }
        
        [TestCase(-1)]
        public async Task Test_GetRanking_All(int guildId) {
            var context = GetRankingTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var characterCount = context.Characters.Count();
            var actionResult   = await controller.GetRanking(guildId);

            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var ranking = okResult.Value as CharacterRankingDTO;
            Assert.NotNull(ranking);

            Assert.AreEqual(characterCount, ranking.Ranking.Count);

            var previous = Int32.MaxValue;
            foreach (var character in ranking.Ranking) {
                Assert.LessOrEqual(character.TotalXP, previous);
                previous = character.TotalXP;
            }
        }
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Test_GetRanking_ValidGuildId(int guildId) {
            var context = GetRankingTestsDatabaseContext();
            var controller   = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var characterCount = context.Characters.Count(character => character.GuildId == guildId);
            var actionResult   = await controller.GetRanking(guildId);

            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var ranking = okResult.Value as CharacterRankingDTO;
            Assert.NotNull(ranking);

            Assert.AreEqual(characterCount, ranking.Ranking.Count);
            
            var previous = Int32.MaxValue;
            foreach (var character in ranking.Ranking) {
                Assert.LessOrEqual(character.TotalXP, previous);
                previous = character.TotalXP;
            }
        }
    }
}