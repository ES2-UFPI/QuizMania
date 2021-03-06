﻿using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionReadDTO>()
                .ForMember(dest => dest.HasMultipleCorrectAnswers, opt => opt.MapFrom<QuestionReadDTO.MultipleCorrectAnswersResolver>());
            CreateMap<SaveQuiz_QuestionDTO, Question>();
            CreateMap<SaveQuestion_QuestionDTO, Question>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
