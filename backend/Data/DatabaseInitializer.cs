using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizMania.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.Data
{
    public static class DatabaseInitializer
    {
        private static async Task ContextSeeder(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            // mock characters
            context.Add(new Character()
            {
                Id = 1,
                Name = "Gandalf",
                TotalXP = 5,
                Gold = 10,
                HealthPoints = 100,
            });

            context.Add(new Character()
            {
                Id = 2,
                Name = "Jurema",
                TotalXP = 55,
                Gold = 70,
                HealthPoints = 80,
            });

            // mock quizzes
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
            };

            var question2 = new Question()
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
            };

            var question3 = new Question()
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
            };

            quiz1.Questions.Add(question1);
            quiz1.Questions.Add(question2);
            quiz1.Questions.Add(question3);

            quiz2.Questions.Add(question3);
            quiz2.Questions.Add(question2);

            context.Quizzes.Add(quiz1);
            context.Quizzes.Add(quiz2);

            await context.SaveChangesAsync();
        }

        public static async Task SeedAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<DatabaseContext>())
            {
                await ContextSeeder(context);
            }
        }
    }
}
