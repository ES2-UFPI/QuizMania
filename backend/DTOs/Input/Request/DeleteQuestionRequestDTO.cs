namespace QuizMania.WebAPI.DTOs.Input
{
    public class DeleteQuestionRequestDTO
    {
        public long CharacterId { get; set; }

        public long QuizId { get; set; }

        public long QuestionId { get; set; }
    }
}
