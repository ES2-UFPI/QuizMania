using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class Quiz
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public ICollection<Question> Questions { get; set; }
    }
}
