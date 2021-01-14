using QuizMania.WebAPI.DTOs.Input;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class DeleteQuestionResponseDTO
    {
        public enum RequestResult
        {
            BadRequest,
            Success,
            CharacterNotOwner,
            QuizNotFound,
            QuestionNotFound
        }

        internal RequestResult _result { get; set; }
        public string Result => _result.ToString();
        public DeleteQuestionRequestDTO Request { get; set; }
    }
}
