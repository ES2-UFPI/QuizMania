using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.Models
{
    public class QuizFeedback
    {
        public QuizFeedback()
        {
            QuestionAnswers = new HashSet<QuestionAnswer>();
        }

        [Key]
        public long Id { get; set; }
        
        [Required]
        public Quiz Quiz { get; set; }

        [Required]
        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        [Required]
        public int GoldGained { get; set; }

        [Required]
        public int ExperienceGained { get; set; }
    }
}
