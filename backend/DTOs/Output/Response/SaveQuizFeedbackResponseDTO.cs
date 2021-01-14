using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class SaveQuizFeedbackResponseDTO
    {
        public enum RequestResult
        {
            BadRequest,
            Success,
            CharacterNotFound,
            InvalidQuizFeedback,
            QuizNotFound,
            QuestionNotFound,
            AnswerNotFound
        }

        internal RequestResult _result { get; set; }

        public string Result => _result.ToString();

        public QuizFeedbackReadDTO QuizFeedback { get; set; }
    }
}
