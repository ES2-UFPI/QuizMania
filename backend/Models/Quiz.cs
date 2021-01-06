using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class Quiz
    {
        public Quiz()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
