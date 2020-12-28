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
        private readonly IMapper _mapper;

        public CharacterController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterInfoDTO>> GetCharacter(long id)
        {
            var character = await _characterService.GetCharacterAsync(id);
            return character != null ? Ok(_mapper.Map<CharacterInfoDTO>(character)) : NotFound();
        }
    }
}