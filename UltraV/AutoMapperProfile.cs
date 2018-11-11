namespace UltraV
{
    using AutoMapper;
    using Domain.Domain;
    using Domain.Models;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<CharacterViewModel, Character>()
                .ForMember(dest => dest.ItemLevel, opt => opt.MapFrom(src => src.Items.AverageItemLevelEquipped))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Spec.Role))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Spec.Name));
            
            this.CreateMap<Character, ItemViewModel>()
                .ForMember(dest => dest.AverageItemLevelEquipped, opt => opt.MapFrom(src => src.ItemLevel));

            this.CreateMap<Character, SpecViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Specialization))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            this.CreateMap<Character, CharacterViewModel>()
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.CharacterPicture))
                .ForMember(dest => dest.Spec, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CharacterName));
        }
    }
}