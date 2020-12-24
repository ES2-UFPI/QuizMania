using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI.DTOs
{
    public class QuestionAnswerReadDTO
    {
        public virtual QuestionReadDTO Question { get; set; }
        public virtual ICollection<long> ChosenAnswerIds { get; set; }

        public QuestionAnswerReadDTO()
        {
            ChosenAnswerIds = new HashSet<long>();
        }
    }
}
