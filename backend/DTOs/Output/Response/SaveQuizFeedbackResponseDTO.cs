namespace QuizMania.WebAPI.DTOs.Output
{
    public class SaveQuizFeedbackResponseDTO
    {
        public enum RequestResult
        {
            BadRequest,
            Success,
            InvalidQuizFeedback,
            QuizWithoutQuestions,
            CharacterNotFound,
            QuizNotFound,
            QuestionNotFound,
            AnswerNotFound
        }

        internal RequestResult _result { get; set; }

        public string Result => _result.ToString();

        public QuizFeedbackReadDTO QuizFeedback { get; set; }
    }
}
