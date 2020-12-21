using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.DTOs;
using AutoMapper;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;
using System.Linq;

namespace QuizMania.WebAPI.Controllers
{
    [Route("/character")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterAsyncRepository _characterRepo;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterAsyncRepository characterRepo, IMapper mapper)
        {
            _characterRepo = characterRepo;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharacters()
        //{
        //    var characters = await _characterRepo.GetAllCharactersAsync();
        //    return Ok(_mapper.Map<IEnumerable<CharacterReadDTO>>(characters));
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacter(long id)
        {
            var character = await _characterRepo.GetCharacterAsync(id);
            return character != null ? Ok(_mapper.Map<CharacterReadDTO>(character)) : NotFound();
        }
    }
}
