using AutoMapper;
using LearningMaterials.Entities;
using LearningMaterials.Models;

namespace LearningMaterials.Profiles
{
    public class MaterialsMappingProfile : Profile
    {
        public MaterialsMappingProfile()
        {
            CreateMap<Author, AuthorReadDto>();
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
        }
    }
}
