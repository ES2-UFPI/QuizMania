using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs
{
    public class QuestionAnswerReceivedDTO
    {
        public long QuestionId { get; set; }

        public ICollection<long> ChosenAnswerIds { get; set; }

        public QuestionAnswerReceivedDTO()
        {
            ChosenAnswerIds = new HashSet<long>();
        }
    }
}
