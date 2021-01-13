using System;

namespace QuizMania.WebAPI.DTOs
{
    public class DeleteQuizRequestResultDTO
    {
        public enum RequestResult
        {
            BadRequest,
            Success,
            CharacterNotOwner,
            QuizNotFound
        }

        internal RequestResult _result { get; set; }

        public DeleteQuizRequestDTO Request { get; set; }
        public string Result => _result.ToString();
    }
}
