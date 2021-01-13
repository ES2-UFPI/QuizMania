using System.Collections.Generic;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class QuizReadDTO
    {
        public long Id { get; set; }
        public Character Owner { get; set; }
        public virtual ICollection<QuestionReadDTO> Questions { get; set; }

        public QuizReadDTO()
        {
            Questions = new HashSet<QuestionReadDTO>();
        }
    }
}
