using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class Answer
    {
        public Answer()
        {
            QuestionAnswers = new HashSet<QuestionAnswer>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        [MaxLength(256)]
        public string Text { get; set; }

        [Required]
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
