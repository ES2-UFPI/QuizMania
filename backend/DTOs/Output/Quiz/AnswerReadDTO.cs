﻿namespace QuizMania.WebAPI.DTOs.Output
{
    public class AnswerReadDTO
    {
        public long Id { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
    }
}
