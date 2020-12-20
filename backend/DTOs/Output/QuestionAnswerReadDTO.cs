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
        public virtual ICollection<long> AnswersId { get; set; }

        public QuestionAnswerReadDTO()
        {
            AnswersId = new HashSet<long>();
        }

    }
}
