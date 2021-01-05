using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using QuizMania.WebAPI.DTOs;
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

        [HttpPost("/expendGold")]
        public async Task<ActionResult<GoldExpenseRequestResultDTO>> TryExpendGold(GoldExpenseRequestDTO expenseRequest)
        {
            var result = await _characterService.TryExpendGold(expenseRequest);
            return result != null ? Ok(result) : NotFound();
        }
    }
}