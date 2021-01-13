using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class QuizFeedbackReadDTO
    {
        public long CharacterId { get; set; }

        public long QuizId { get; set; }

        public ICollection<QuestionAnswerReadDTO> QuestionAnswers { get; set; }

        public float PercentageOfCorrectChosenAnswers { get; set; }

        public int GoldGained { get; set; }

        public int ExperienceGained { get; set; }

        public int LevelGained { get; set; }
    }
}
