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
using System.Collections.Generic;
using System.Linq;

namespace QuizMania.WebAPI.Tests.Controllers
{
    partial class CharacterControllerTests
    {
        private DatabaseContext GetItemTestsDatabaseContext()
        {
            var context = GetUniqueDatabaseContext("ItemTestsDatabaseContext");
            context.Database.EnsureDeletedAsync().Wait();
            context.Database.EnsureCreatedAsync().Wait();

            var infinite = new ItemInfo()
            {
                Name = "Infinite Item",
                Cost = 3,
                Type = SlotType.Other,
                MaxQuantity = -1,
            }; context.Items.Add(infinite);

            var blackShoe2 = new ItemInfo()
            {
                Name = "blackShoe2.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blackShoe2);

            var tint3_hand = new ItemInfo()
            {
                Name = "tint3_hand.png",
                Cost = 8,
                Type = SlotType.Hand,
                MaxQuantity = 1,
            }; context.Items.Add(tint3_hand);

            var tint5_neck = new ItemInfo()
            {
                Name = "tint5_neck.png",
                Cost = 8,
                Type = SlotType.Neck,
                MaxQuantity = 1,
            }; context.Items.Add(tint5_neck);

            var armYellow_long = new ItemInfo()
            {
                Name = "armYellow_long.png",
                Cost = 8,
                Type = SlotType.Arm,
                MaxQuantity = 1,
            }; context.Items.Add(armYellow_long);

            var pantsGreen_long = new ItemInfo()
            {
                Name = "pantsGreen_long.png",
                Cost = 10,
                Type = SlotType.Pants,
                MaxQuantity = 1,
            }; context.Items.Add(pantsGreen_long);

            var blueShoe5 = new ItemInfo()
            {
                Name = "blueShoe5.png",
                Cost = 15,
                Type = SlotType.Shoe,
                MaxQuantity = 1,
            }; context.Items.Add(blueShoe5);

            var tint6_hand = new ItemInfo()
            {
                Name = "tint6_hand.png",
                Cost = 8,
                Type = SlotType.Hand,
                MaxQuantity = 1,
            }; context.Items.Add(tint6_hand);

            var char1 = new Character { Gold = 3000 };
            
            context.Characters.Add(char1);

            char1.Items.Add(new InventoryItem(blackShoe2, 1, isEquipped: true));
            char1.Items.Add(new InventoryItem(blueShoe5, 1, isEquipped: false));
            char1.Items.Add(new InventoryItem(tint3_hand, 1, isEquipped: true));
            char1.Items.Add(new InventoryItem(tint5_neck, 1, isEquipped: true));
            char1.Items.Add(new InventoryItem(armYellow_long, 1, isEquipped: true));
            char1.Items.Add(new InventoryItem(pantsGreen_long, 1, isEquipped: false));

            context.SaveChanges();

            return context;
        }

        [Test]
        public async Task Test_GetItems_Success()
        {
            var context = GetItemTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var itemCount = await context.Items.CountAsync(); 

            var actionResult = await controller.GetItems();
            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okResult = actionResult as OkObjectResult;
            var itemsInfoDTO = okResult.Value as ICollection<ItemInfoDTO>;

            Assert.NotNull(itemsInfoDTO);

            Assert.AreEqual(itemCount, itemsInfoDTO.Count);
        }

        [TestCase(1)]
        public async Task Test_GetCharacterItems_Success(long value)
        {
            var context = GetItemTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var characterItems = await context.Characters.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == value);

            var actionResult = await controller.GetCharacterItems(value);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okResult = actionResult as OkObjectResult;
            var characterItemsDTO = okResult.Value as CharacterItemsDTO;
            
            Assert.NotNull(characterItemsDTO);

            Assert.IsTrue(characterItemsDTO.CharacterId == value);
            Assert.AreEqual(characterItems.Items.Count, characterItemsDTO.Items.Count);   
        }

        [TestCase(999)]
        public async Task Test_GetCharacterItems_NotFound(long value)
        {
            var context = GetItemTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.GetCharacterItems(value);

            Assert.IsInstanceOf<NotFoundResult>(actionResult);    
        }

        [TestCase(1, "pantsGreen_long.png")]
        [TestCase(1, "blueShoe5.png")]
        public async Task Test_EquipItem_Success(long characterId, string itemName)
        {
            var context = GetItemTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.Un_EquipItem(new Un_EquipItemRequestDTO { CharacterId = characterId, ItemName = itemName });

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var charItems = await context.Characters.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == characterId);

            var item = charItems.Items.FirstOrDefault(i => i.Item.Name == itemName && i.IsEquipped);
            
            var test = charItems.Items.Where(i => i.Item.Type == item.Item.Type && i.IsEquipped).ToList().Count;

            Assert.NotNull(item);
            Assert.IsTrue(test == 1);

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as Un_EquipItemResponseDTO;

            Assert.NotNull(response);
            Assert.AreEqual(Un_EquipItemResponseDTO.RequestResult.Success.ToString(), response.Result);
        }

        [TestCase(1, "blackShoe2.png")]
        public async Task Test_UnequipItem_Success(long characterId, string itemName)
        {
            var context = GetItemTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.Un_EquipItem(new Un_EquipItemRequestDTO { CharacterId = characterId, ItemName = itemName });

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var item = (await context.Characters.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == characterId))
                                                .Items.FirstOrDefault(i => i.Item.Name == itemName && i.IsEquipped);
            Assert.IsNull(item);

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as Un_EquipItemResponseDTO;

            Assert.NotNull(response);
            Assert.AreEqual(Un_EquipItemResponseDTO.RequestResult.Success.ToString(), response.Result);
        }

        [TestCase(1, "tint6_hand.png")]
        public async Task Test_Un_EquipItem_InventoryWithoutItem(long characterId, string itemName)
        {
            var context = GetItemTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.Un_EquipItem(new Un_EquipItemRequestDTO { CharacterId = characterId, ItemName = itemName });

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }

        [TestCase(1, "tint6_hand.png", 1)]
        [TestCase(1, "tint6_hand.png", 2)]
        public async Task Test_TryPurchaseItem_Success(long characterId, string itemName, int quantity)
        {
            var context = GetItemTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.TryPurchaseItem(new ItemPurchaseRequestDTO{ CharacterId = characterId, ItemName = itemName, Quantity = quantity });

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var item = (await context.Characters.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == characterId))
                                                .Items.FirstOrDefault(i => i.Item.Name == itemName);
            Assert.AreEqual(quantity, item.Quantity);

            var okResult = actionResult as OkObjectResult;
            var response = okResult.Value as ItemPurchaseResponseDTO;

            Assert.NotNull(response);
            Assert.AreEqual(GoldExpenseResult.Authorized.ToString(), response.ResultMessage);
        }

        [TestCase(1, "Non Existent", 1)]
        public async Task Test_TryPurchaseItem_ItemNotFound(long characterId, string itemName, int quantity)
        {
            var context = GetItemTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.TryPurchaseItem(new ItemPurchaseRequestDTO { CharacterId = characterId, ItemName = itemName, Quantity = quantity });

            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }

        [TestCase(1, "Infinite Item", 9999)]
        [TestCase(1, "blackShoe2.png", 1)]
        public async Task Test_TryPurchaseItem_NotEnoughResourcesOrReachedItemMaxQuantity(long characterId, string itemName, int quantity)
        {
            var context = GetItemTestsDatabaseContext();
            var controller = new CharacterController(new CharacterService(new CharacterRepository(context), new ItemRepository(context), Mapper));

            var actionResult = await controller.TryPurchaseItem(new ItemPurchaseRequestDTO { CharacterId = characterId, ItemName = itemName, Quantity = quantity });

            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }
    }
}