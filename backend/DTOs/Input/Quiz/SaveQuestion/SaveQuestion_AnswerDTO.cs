﻿using System.ComponentModel.DataAnnotations;


namespace QuizMania.WebAPI.DTOs.Input
{
    public class SaveQuestion_AnswerDTO
    {
        public bool IsCorrect { get; set; }

        [Required]
        [MaxLength(256)]
        public string Text { get; set; }
    }
}
