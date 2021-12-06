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

            CreateMap<MaterialType, MaterialTypeReadDto>();
            CreateMap<MaterialTypeCreateDto, MaterialType>();
            CreateMap<MaterialTypeUpdateDto, MaterialType>().ReverseMap();

            CreateMap<Material, MaterialReadDto>()
                .ForMember(m => m.AuthorName, m => m.MapFrom(m => m.Author.Name))
                .ForMember(m => m.MaterialTypeName, m => m.MapFrom(m => m.MaterialType.Name));
            CreateMap<MaterialCreateDto, Material>();
            CreateMap<MaterialUpdateDto, Material>().ReverseMap();

            CreateMap<Review, ReviewReadDto>()
                .ForMember(m => m.MaterialTitle, m => m.MapFrom(m => m.Material.Title));
            CreateMap<ReviewCreateDto, Review>();
            CreateMap<ReviewUpdateDto, Review>().ReverseMap();
        }
    }
}
