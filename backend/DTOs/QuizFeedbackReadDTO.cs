using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs
{
    public class QuizFeedbackReadDTO : QuizFeedbackReceivedDTO
    {
        public int GoldGained { get; set; }

        public int ExperienceGained { get; set; }
    }
}
