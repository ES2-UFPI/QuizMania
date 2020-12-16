using System.Collections.Generic;
using QuizMania.WebAPI.Models;
using AutoMapper;

namespace QuizMania.WebAPI.DTOs
{
    public class QuestionReadDTO
    {
        public class MultipleCorrectAnswersResolver : IValueResolver<Question, QuestionReadDTO, bool>
        {
            public bool Resolve(Question source, QuestionReadDTO destination, bool destMember, ResolutionContext context)
            {
                var answers = source.Answers;
                var hasAnswer = false;
                foreach (var ans in answers)
                {
                    if (ans.IsCorrect)
                    {
                        if (hasAnswer)
                        {
                            // question has at least 2 correct answers
                            return true;
                        }
                        else
                        {
                            hasAnswer = true;
                        }
                    }
                }

                return false;
            }
        }

        public long Id { get; set; }
        public string Text { get; set; }
        public bool HasMultipleCorrectAnswers { get; set; }
        public virtual ICollection<AnswerReadDTO> Choices { get; set; }

        public QuestionReadDTO()
        {
            Choices = new HashSet<ChoiceReadDTO>();
        }
    }
}
