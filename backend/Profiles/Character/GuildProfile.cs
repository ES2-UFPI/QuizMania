﻿using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.DTOs.Input;
using QuizMania.WebAPI.DTOs.Output;

namespace QuizMania.WebAPI.Profiles
{
    public class GuildProfile : Profile
    {
        public GuildProfile()
        {
            CreateMap<Guild, GuildInfoDTO>();
        }
    }
}
