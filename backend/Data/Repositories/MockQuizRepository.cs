using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class MockQuizRepository : IQuizAsyncRepository
    {
        private readonly QuizContext _context;

        public MockQuizRepository(QuizContext context)
        {
            _context = context;

            var quiz1 = new Quiz()
            {
                Id = 1,
            };

            var quiz2 = new Quiz()
            {
                Id = 2,
            };

            var question1 = new Question()
            {
                Id = 1,
                Text = "What is the answer to the meaning of life, the universe and everything?",
                Choices = new List<Choice>()
                {
                    new Choice()
                    {
                        Id = 1,
                        IsCorrect = false,
                        Text = "40",
                    },

                    new Choice()
                    {
                        Id = 2,
                        IsCorrect = false,
                        Text = "41",
                    },

                    new Choice()
                    {
                        Id = 3,
                        IsCorrect = true,
                        Text = "42",
                    },

                    new Choice()
                    {
                        Id = 4,
                        IsCorrect = false,
                        Text = "43",
                    },
                }
            };

            var question2 = new Question()
            {
                Id = 2,
                Text = "This is a true or false question. True or False?",
                Choices = new List<Choice>()
                        {
                            new Choice()
                            {
                                Id = 5,
                                IsCorrect = true,
                                Text = "True",
                            },

                            new Choice()
                            {
                                Id = 6,
                                IsCorrect = false,
                                Text = "False",
                            },
                        }
            };

            var question3 = new Question()
            {
                Id = 3,
                Text = "All options are correct. Which options are correct?",
                Choices = new List<Choice>()
                        {
                            new Choice()
                            {
                                Id = 7,
                                IsCorrect = true,
                                Text = "A",
                            },

                            new Choice()
                            {
                                Id = 8,
                                IsCorrect = true,
                                Text = "B",
                            },
                        }
            };

            quiz1.Questions.Add(question1);
            quiz1.Questions.Add(question2);
            quiz1.Questions.Add(question3);

            quiz2.Questions.Add(question3);
            quiz2.Questions.Add(question2);

            question1.Quizzes.Add(quiz1);

            question2.Quizzes.Add(quiz1);
            question2.Quizzes.Add(quiz2);

            question3.Quizzes.Add(quiz1);
            question3.Quizzes.Add(quiz2);

            _context.Quizzes.Add(quiz1);
            _context.Quizzes.Add(quiz2);

            _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Quiz>> GetAllQuizzesAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task<Quiz> GetQuizAsync(long id)
        {
            return await _context.Quizzes.FindAsync(id);
        }
    }
}