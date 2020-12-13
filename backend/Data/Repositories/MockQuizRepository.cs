using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class MockQuizRepository : IQuizAsyncRepository
    {
        private readonly QuizContext _context;

        public MockQuizRepository(QuizContext context) {
            _context = context;

            _context.Quizzes.Add(new Quiz
            {
                Id = 1,
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        Id = 1,
                        Text = "What is the answer to the meaning of life, the universe and everything?",
                        Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                Id = 1,
                                IsCorrect = false,
                                Text = "40",
                            },

                            new Answer()
                            {
                                Id = 2,
                                IsCorrect = false,
                                Text = "41",
                            },

                            new Answer()
                            {
                                Id = 3,
                                IsCorrect = true,
                                Text = "42",
                            },

                            new Answer()
                            {
                                Id = 4,
                                IsCorrect = false,
                                Text = "43",
                            },
                        }
                    },

                    new Question()
                    {
                        Id = 2,
                        Text = "This is a true or false question. True or False?",
                        Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                Id = 5,
                                IsCorrect = true,
                                Text = "True",
                            },

                            new Answer()
                            {
                                Id = 6,
                                IsCorrect = false,
                                Text = "False",
                            },
                        }
                    },

                    new Question()
                    {
                        Id = 3,
                        Text = "All options are correct. Which options are correct?",
                        Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                Id = 7,
                                IsCorrect = true,
                                Text = "A",
                            },

                            new Answer()
                            {
                                Id = 8,
                                IsCorrect = true,
                                Text = "B",
                            },
                        }
                    },
                },
            }) ;

            _context.SaveChangesAsync();
        }


        public async Task<ActionResult<IEnumerable<Quiz>>> GetAllQuizzesAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task<ActionResult<Quiz>> GetQuizAsync(long id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return new NotFoundResult();
            }

            return quiz;
        }
    }
}
