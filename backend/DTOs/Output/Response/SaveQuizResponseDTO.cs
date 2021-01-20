namespace QuizMania.WebAPI.DTOs.Output
{
    public class SaveQuizResponseDTO
    {
        public enum RequestResult
        {
            BadRequest,
            Success,
            EmptyAtribute,
            QuestionWithoutCorrectAnswer,
            OwnerNotFound
        }

        internal RequestResult _result { get; set; }

        public string Result => _result.ToString();

        public QuizReadDTO Quiz { get; set; }
    }
}
