﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class SqlCharacterRepository : ICharacterAsyncRepository
    {
        private readonly DatabaseContext _context;

        public SqlCharacterRepository (DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character> GetCharacterAsync(long id)
        {
            return await _context.Characters.Include(c => c.QuizFeedbacks)
                                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync(); ;
        }
    }
}
