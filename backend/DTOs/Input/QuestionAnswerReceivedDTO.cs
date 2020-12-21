using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class QuestionAnswerReceivedDTO
    {
        public long QuestionId { get; set; }

        public ICollection<long> AnswerIds { get; set; }

        public QuestionAnswerReceivedDTO()
        {
            AnswerIds = new HashSet<long>();
        }
    }
}
