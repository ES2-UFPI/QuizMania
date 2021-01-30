using System.Collections.Generic;
using System.Threading.Tasks;
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


        /// <remarks>
        /// <h2> **Description:** </h2>
        /// <h3> Retorna um character com suas informacoes básicas. </h3>
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterInfoDTO>> GetCharacter(long id)
        {
            var character = await _characterService.GetCharacterInfoAsync(id);
            return character != null ? Ok(character) : NotFound();
        }


        /// <remarks>
        /// <h2> **Description:** </h2>
        /// <h3> Retorna todos os itens da loja. </h3>
        /// </remarks>
        [HttpGet("items")]
        public async Task<ActionResult<IEnumerable<ItemInfoDTO>>> GetItems()
        {
            var items = await _characterService.GetItemsAsync();
            return items != null ? Ok(items) : NotFound();
        }


        /// <remarks>
        /// <h2> **Description:** </h2>
        /// <h3> Retorna um character com seus itens. </h3>
        /// </remarks>
        [HttpGet("items/{id}")]
        public async Task<ActionResult<CharacterItemsDTO>> GetCharacterItems(long id)
        {
            var character = await _characterService.GetCharacterItemsAsync(id);
            return character != null ? Ok(character) : NotFound();
        }


        


        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Sucess </h3>
        /// <h3> **404:** CharacterNotFound, GuildNotFound </h3>
        /// <h3> **400:** BadRequest </h3>
        /// <h2> **Description:** </h2>
        /// <h3> O character especificado entra/sai da guild informada, entra na guild 
        ///      caso ele não esteja nela (por consequencia saindo da guilda que 
        ///      ele esta, caso ele ja esteja em alguma) e sai caso contrário. </h3>
        /// </remarks>
        [HttpPatch("guilds")]
        public async Task<ActionResult<Leave_JoinGuildResponseDTO>> Leave_JoinGuild(Leave_JoinGuildRequestDTO leave_joinRequest)
        {
            var result = await _characterService.Leave_JoinGuilddAsyc(leave_joinRequest);
            switch (result._result)
            {
                case Leave_JoinGuildResponseDTO.RequestResult.Success: return Ok(result);
                case Leave_JoinGuildResponseDTO.RequestResult.CharacterNotFound: return NotFound(result);
                case Leave_JoinGuildResponseDTO.RequestResult.GuildNotFound: return NotFound(result);
                default: return BadRequest(result);
            }
        }


        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Sucess </h3>
        /// <h3> **404:** CharacterNotFound </h3>
        /// <h3> **400:** BadRequest, InventoryWithoutItem </h3>
        /// <h2> **Description:** </h2>
        /// <h3> Equipa/desequipa o item especificado do character informado, equipa o 
        ///      item caso ele não esteja equipado e desequipa caso contrário. </h3>
        /// </remarks>
        [HttpPatch("items")]
        public async Task<ActionResult<Un_EquipItemResponseDTO>> Un_EquipItem(Un_EquipItemRequestDTO un_equipItemRequest)
        {
            var result = await _characterService.Un_EquipItemAsync(un_equipItemRequest);
            switch (result._result)
            {
                case Un_EquipItemResponseDTO.RequestResult.Success: return Ok(result);
                case Un_EquipItemResponseDTO.RequestResult.CharacterNotFound: return NotFound(result);
                default: return BadRequest(result);
            }
        }


        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Authorized </h3>
        /// <h3> **404:** CharacterNotFound </h3>
        /// <h3> **400:** BadRequest, NotEnoughResources </h3>
        /// <h2> **Description:** </h2>
        /// <h3> Gasta a quantidade de ouro especificada do character informado. </h3>
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
        /// <h2> **Description:** </h2>
        /// <h3> O item especificado é comprado pelo character informado. </h3>
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