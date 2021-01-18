﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;
using QuizMania.WebAPI.Services;


namespace QuizMania.WebAPI.Controllers
{
    [Route("/character")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterInfoDTO>> GetCharacter(long id)
        {
            var character = await _characterService.GetCharacterInfoAsync(id);
            return character != null ? Ok(character) : NotFound();
        }

        [HttpGet("items/{id}")]
        public async Task<ActionResult<CharacterItemsDTO>> GetCharacterItems(long id)
        {
            var character = await _characterService.GetCharacterItemsAsync(id);
            return character != null ? Ok(character) : NotFound();
        }

        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Authorized </h3>
        /// <h3> **404:** CharacterNotFound </h3>
        /// <h3> **400:** BadRequest, NotEnoughResources </h3>
        /// </remarks>
        [HttpPost("expendGold")]
        public async Task<ActionResult<GoldExpenseResponseDTO>> TryExpendGold(GoldExpenseRequestDTO expenseRequest)
        {
            var result = await _characterService.TryExpendGold(expenseRequest);
            switch (result.Result)
            {
                case GoldExpenseResult.Authorized: return Ok(result);
                case GoldExpenseResult.CharacterNotFound: return NotFound(result);
                default: return BadRequest(result);
            }
        }

        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Authorized </h3>
        /// <h3> **404:** ItemNotFound, CharacterNotFound </h3>
        /// <h3> **400:** BadRequest, NotEnoughResources, ItemNotFound, ReachedItemMaxQuantity </h3>
        /// </remarks>
        [HttpPost("items/purchase")]
        public async Task<ActionResult<ItemPurchaseResponseDTO>> TryPurchaseItem(ItemPurchaseRequestDTO purchaseRequest)
        {
            var result = await _characterService.TryPurchaseItem(purchaseRequest);
            switch (result.Result)
            {
                case GoldExpenseResult.Authorized: return Ok(result);
                case GoldExpenseResult.ItemNotFound:
                case GoldExpenseResult.CharacterNotFound: return NotFound(result);
                default: return BadRequest(result);
            }
        }
    }
}