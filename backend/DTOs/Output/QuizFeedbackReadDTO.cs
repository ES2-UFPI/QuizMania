using QuizMania.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs
{
    public class QuizFeedbackReadDTO
    {
        public long QuizId { get; set; }

        public ICollection<QuestionAnswerReadDTO> QuestionAnswers { get; set; }

        public float PercentageOfCorrectChosenAnswers { get; set; }

        public int GoldGained { get; set; }

        public int ExperienceGained { get; set; }
    }
}
