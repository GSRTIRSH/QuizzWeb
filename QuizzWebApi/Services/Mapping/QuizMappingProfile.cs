using AutoMapper;
using QuizzWebApi.Models;
using QuizzWebApi.Models.Dto;

namespace QuizzWebApi.Services.Mapping;

public class QuizMappingProfile : Profile
{
    public QuizMappingProfile()
    {
        CreateMap<QuizV2, QuizV2Dto>()
            .ForMember(dest => dest.Author, d => d.MapFrom(dest => dest.Author))
            .ForMember(dest => dest.Title, d => d.MapFrom(dest => dest.Title))
            .ForMember(dest => dest.Category, d => d.MapFrom(dest => dest.Category))
            .ForMember(dest => dest.Difficulty, d => d.MapFrom(dest => dest.Difficulty))
            ;
        CreateMap<QuizV2Dto, QuizV2>();
        
        
    }
}