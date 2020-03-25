using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using FirstProject.Domain;

namespace FirstProject.Models.DTOs
{
	public class AutoMapper : Profile
	{
		public AutoMapper()
		{
			CreateMap<FuelWise, FuelWiseDTO>()
				.ForMember(dest =>
				dest.Title,
				opt => opt.MapFrom(src => src.Title))
				 .ForMember(dest =>
				 dest.Body,
				 opt => opt.MapFrom(src => src.Body));

			CreateMap<ArticlesDTO, ArticleDTO>()
				.ForMember(dest =>
				dest.Title,
				opt => opt.MapFrom(src => src.Title))
				.ForMember(dest =>
				dest.Description,
				opt => opt.MapFrom(src => src.Description))
				.ForMember(dest =>
				dest.Content,
				opt => opt.MapFrom(src => src.Content))
				.ForMember(dest =>
				dest.Image,
				opt => opt.MapFrom(src => src.Image));

			CreateMap<Author, AuthorDTO>()
				.ForMember(dest =>
				dest.Name,
				opt => opt.MapFrom(src => src.Name))
				.ForMember(dest =>
				dest.Surname,
				opt => opt.MapFrom(src => src.Surname))
				.ForMember(dest =>
				dest.JobTitle,
				opt => opt.MapFrom(src => src.JobTitle))
				.ForMember(dest =>
				dest.DriverExperience,
				opt => opt.MapFrom(src => src.DriverExperience));

			CreateMap<FileStorage, FileStorageDTO>()
				.ForMember(dest =>
				dest.Name,
				opt => opt.MapFrom(src => src.Name))
				.ForMember(dest =>
				dest.FilePath,
				opt => opt.MapFrom(src => src.FilePath))
				.ForMember(dest =>
				dest.ContentType,
				opt => opt.MapFrom(src => src.ContentType));

			CreateMap<FuelWise, FuelWiseDTO>()
				.ForMember(dest =>
				dest.Title,
				opt => opt.MapFrom(src => src.Title))
				.ForMember(dest =>
				dest.Body,
				opt => opt.MapFrom(src => src.Body));

			CreateMap<TipsForEveryOne, TipsForEveryOneDTO>()
				.ForMember(dest =>
				dest.Name,
				opt => opt.MapFrom(src => src.Name))
				.ForMember(dest =>
				dest.Content,
				opt => opt.MapFrom(src => src.Content));
		}
	}
}
