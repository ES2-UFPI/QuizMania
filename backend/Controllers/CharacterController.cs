using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;
using QuizMania.WebAPI.Services;

namespace QuizMania.WebAPI.Controllers {
    [Route("/character")]
    [ApiController]
    public class CharacterController : ControllerBase {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService) {
            _characterService = characterService;
        }


        /// <remarks>
        /// <h2> **Description:** </h2>
        /// <h3> Retorna um character com suas informacoes básicas. </h3>
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CharacterInfoDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCharacter(long id) {
            var character = await _characterService.GetCharacterInfoAsync(id);

            if (character == null) {
                return NotFound();
            }

            return Ok(character);
        }


        /// <remarks>
        /// <h2> **Description:** </h2>
        /// <h3> Retorna todos os itens da loja. </h3>
        /// </remarks>
        [HttpGet("items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemInfoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetItems() {
            var items = await _characterService.GetItemsAsync();

            if (items == null) {
                return NotFound();
            }

            return Ok(items);
        }


        /// <remarks>
        /// <h2> **Description:** </h2>
        /// <h3> Retorna um character com seus itens. </h3>
        /// </remarks>
        [HttpGet("items/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CharacterItemsDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCharacterItems(long id) {
            var characterItems = await _characterService.GetCharacterItemsAsync(id);

            if (characterItems == null) {
                return NotFound();
            }

            return Ok(characterItems);
        }

        /// <summary>
        /// Retrieves a collection of Characters in descending order of total experience (XP).
        /// </summary>
        /// <param name="guildId">
        /// <para>The ID of the Guild from which Characters will be retrieved.</para>
        /// <para>Use -1 to query any and all Characters.</para>
        /// <para>Use 0 to query Characters that are not part of a Guild.</para>
        /// </param>
        /// <returns>
        /// <see cref="BadRequestResult"/> if <paramref name="guildId"/> is lesser than -1.
        /// <see cref="OkObjectResult"/> otherwise, with a <see cref="CharacterRankingDTO"/> as object value.
        /// </returns>
        [HttpGet("ranking/{guildId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CharacterRankingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRanking(long guildId = -1) {
            var ranking = await _characterService.GetRanking(guildId);

            if (ranking == null) {
                return BadRequest();
            }

            return Ok(ranking);
        }

        /// <remarks>
        /// <h2> **Description:** </h2>
        /// <h3> Retorna todas as guildas. </h3>
        /// </remarks>
        [HttpGet("guilds")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GuildInfoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGuilds()
        {
            var guilds = await _characterService.GetGuildsAsync();
            
            if (guilds == null)
            {
                return NotFound();
            }

            return Ok(guilds);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Leave_JoinGuildResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Leave_JoinGuildResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Leave_JoinGuildResponseDTO))]
        public async Task<IActionResult> Leave_JoinGuild(Leave_JoinGuildRequestDTO leave_joinRequest)
        {
            var result = await _characterService.Leave_JoinGuilddAsyc(leave_joinRequest);
            switch (result._result)
            {
                case Leave_JoinGuildResponseDTO.RequestResult.Success:           return Ok(result);
                case Leave_JoinGuildResponseDTO.RequestResult.CharacterNotFound: return NotFound(result);
                case Leave_JoinGuildResponseDTO.RequestResult.GuildNotFound:     return NotFound(result);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type         = typeof(Un_EquipItemResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type   = typeof(Un_EquipItemResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Un_EquipItemResponseDTO))]
        public async Task<IActionResult> Un_EquipItem(Un_EquipItemRequestDTO un_equipItemRequest) {
            var result = await _characterService.Un_EquipItemAsync(un_equipItemRequest);
            switch (result._result) {
                case Un_EquipItemResponseDTO.RequestResult.Success:           return Ok(result);
                case Un_EquipItemResponseDTO.RequestResult.CharacterNotFound: return NotFound(result);
                default:                                                      return BadRequest(result);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type         = typeof(GoldExpenseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type   = typeof(GoldExpenseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GoldExpenseResponseDTO))]
        public async Task<IActionResult> TryExpendGold(GoldExpenseRequestDTO expenseRequest) {
            var result = await _characterService.TryExpendGold(expenseRequest);
            switch (result.Result) {
                case GoldExpenseResult.Authorized:        return Ok(result);
                case GoldExpenseResult.CharacterNotFound: return NotFound(result);
                default:                                  return BadRequest(result);
            }
        }


        /// <remarks>
        /// <h2> **Result values:** </h2> 
        /// <h3> **200:** Authorized </h3>
        /// <h3> **404:** ItemNotFound, CharacterNotFound </h3>
        /// <h3> **400:** BadRequest, NotEnoughResources, ReachedItemMaxQuantity </h3>
        /// <h2> **Description:** </h2>
        /// <h3> O item especificado é comprado pelo character informado. </h3>
        /// </remarks>
        [HttpPost("items/purchase")]
        [ProducesResponseType(StatusCodes.Status200OK, Type         = typeof(ItemPurchaseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type   = typeof(ItemPurchaseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ItemPurchaseResponseDTO))]
        public async Task<IActionResult> TryPurchaseItem(ItemPurchaseRequestDTO purchaseRequest) {
            var result = await _characterService.TryPurchaseItem(purchaseRequest);
            switch (result.Result) {
                case GoldExpenseResult.Authorized:        return Ok(result);
                case GoldExpenseResult.ItemNotFound:
                case GoldExpenseResult.CharacterNotFound: return NotFound(result);
                default:                                  return BadRequest(result);
            }
        }
    }
}