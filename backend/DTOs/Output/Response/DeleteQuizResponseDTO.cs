using QuizMania.WebAPI.DTOs.Input;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class DeleteQuizResponseDTO
    {
        public enum RequestResult
        {
            BadRequest,
            Success,
            CharacterNotOwner,
            QuizNotFound
        }

        internal RequestResult _result { get; set; }
        public string Result => _result.ToString();
        public DeleteQuizRequestDTO Request { get; set; }
    }
}
