using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs
{
    public class QuestionReadDTO
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public virtual ICollection<ChoiceReadDTO> Choices { get; set; }

        public QuestionReadDTO()
        {
            Choices = new HashSet<ChoiceReadDTO>();
        }
    }
}
