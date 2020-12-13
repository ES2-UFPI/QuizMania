using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class Question
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string Text { get; set; }

        [Required]
        public ICollection<Answer> Answers { get; set; }
    }
}
