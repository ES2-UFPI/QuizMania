using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs
{
    public class QuestionAnswerDTO
    {
        public virtual long QuestionId { get; set; }
        public virtual ICollection<long> Answers { get; set; }

        public QuestionAnswerDTO()
        {
            Answers = new HashSet<long>();
        }

    }
}
