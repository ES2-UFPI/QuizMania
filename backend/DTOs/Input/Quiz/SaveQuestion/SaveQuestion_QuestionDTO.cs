using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class SaveQuestion_QuestionDTO
    {
        public long QuizId { get; set; }
        public string Text { get; set; }
        public virtual ICollection<SaveQuestion_AnswerDTO> Answers { get; set; }
        public SaveQuestion_QuestionDTO()
        {
            Answers = new HashSet<SaveQuestion_AnswerDTO>();
        }

    }
}
