﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizMania.WebAPI.Data;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.Controllers
{
    [Route("/database")]
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly DatabaseInitializer _dbInitializer;

        public DatabaseController(DatabaseInitializer databaseInitializer)
        {
            _dbInitializer = databaseInitializer;
        }

        [HttpGet("reset")]
        public async Task<ActionResult> ResetDB()
        {
            return await _dbInitializer.ContextSeederAsync() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}