using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class QuizFeedback
    {
        public QuizFeedback()
        {
            GoldGained = 0;
            ExperienceGained = 0;
            QuestionAnswers = new HashSet<QuestionAnswer>();
        }

        [Key]
        public long Id { get; set; }
        
        [Required]
        public Quiz Quiz { get; set; }

        [Required]
        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        [Required]
        public float PercentageOfCorrectChosenAnswers { get; set; }

        [Required]
        public int GoldGained { get; set; }

        [Required]
        public int ExperienceGained { get; set; }
    }
}
