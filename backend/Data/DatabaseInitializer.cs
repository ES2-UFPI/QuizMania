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
            var char1 = new Character()
            {
                Name = "Gandalf",
                TotalXP = 5,
                Gold = 10,
                HealthPoints = 100,
            };

            var char2 = new Character()
            {
                Name = "Jurema",
                TotalXP = 55,
                Gold = 70,
                HealthPoints = 80,
            };

            context.Characters.Add(char1);
            context.Characters.Add(char2);

            // mock quizzes
            var quiz1 = new Quiz()
            {
                Owner = char1,
            };

            var quiz2 = new Quiz()
            {
                Owner = char2,
            };

            var question1 = new Question()
            {
                Text = "What is the answer to the meaning of life, the universe and everything?",
                Answers = new List<Answer>()
                {
                    new Answer()
                    {
                        IsCorrect = false,
                        Text = "40",
                    },

                    new Answer()
                    {
                        IsCorrect = false,
                        Text = "41",
                    },

                    new Answer()
                    {
                        IsCorrect = true,
                        Text = "42",
                    },

                    new Answer()
                    {
                        IsCorrect = false,
                        Text = "43",
                    },
                }
            };

            var question2 = new Question()
            {
                Text = "This is a true or false question. True or False?",
                Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                IsCorrect = true,
                                Text = "True",
                            },

                            new Answer()
                            {
                                IsCorrect = false,
                                Text = "False",
                            },
                        }
            };

            var question3 = new Question()
            {
                Text = "All options are correct. Which options are correct?",
                Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                IsCorrect = true,
                                Text = "A",
                            },

                            new Answer()
                            {
                                IsCorrect = true,
                                Text = "B",
                            },
                        }
            };

            quiz1.Questions.Add(question1);

            quiz2.Questions.Add(question2);
            quiz2.Questions.Add(question3);

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
