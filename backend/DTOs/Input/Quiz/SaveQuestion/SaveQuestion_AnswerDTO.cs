using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class SaveQuestion_AnswerDTO
    {
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
    }
}
