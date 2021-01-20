namespace QuizMania.WebAPI.DTOs.Output
{
    public class SaveQuestionResponseDTO
    {
        public enum RequestResult
        {
            BadRequest,
            Success,
            EmptyAtribute,
            QuestionWithoutCorrectAnswer,
            CharacterNotOwner,
            QuizNotFound
        }

        internal RequestResult _result { get; set; }

        public string Result => _result.ToString();

        public QuestionReadDTO Question { get; set; }
    }
}
