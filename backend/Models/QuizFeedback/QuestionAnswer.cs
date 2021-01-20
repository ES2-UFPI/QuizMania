using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class QuestionAnswer
    {
        public QuestionAnswer()
        {
            ChosenAnswers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public QuizFeedback QuizFeedback { get; set; }

        public long? QuestionId { get; set; }

        public Question Question { get; set; }

        [Required]
        public virtual ICollection<Answer> ChosenAnswers { get; set; }
    }
}
