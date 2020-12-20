using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.Models
{
    public class QuestionAnswer
    {
        public QuestionAnswer()
        {
            Answers = new HashSet<long>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public virtual long QuestionId { get; set; }

        [Required]
        public virtual ICollection<long> Answers { get; set; }
    }
}
