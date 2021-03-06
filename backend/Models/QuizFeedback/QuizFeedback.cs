﻿using System.Collections.Generic;
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
        public Character Character { get; set; }

        public long? QuizId { get; set; }

        public Quiz Quiz { get; set; }

        [Required]
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        [Required]
        public float PercentageOfCorrectChosenAnswers { get; set; }

        [Required]
        public int GoldGained { get; set; }

        [Required]
        public int ExperienceGained { get; set; }

        [Required]
        public int LevelGained { get; set; }
    }
}
