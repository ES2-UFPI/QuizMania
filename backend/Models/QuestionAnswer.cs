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
            ChosenAnswers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public Question Question { get; set; }

        [Required]
        public virtual ICollection<Answer> ChosenAnswers { get; set; }
    }
}
