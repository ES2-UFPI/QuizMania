using QuizMania.WebAPI.DTOs.Input;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class Leave_JoinGuildResponseDTO
    {
        public enum RequestResult
        {
            BadRequest,
            Success,
            CharacterNotFound,
            GuildNotFound
        }

        internal RequestResult _result { get; set; }
        public string Result => _result.ToString();
        public Leave_JoinGuildRequestDTO Request { get; set; }
    }
}
