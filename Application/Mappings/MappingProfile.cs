using Application.DTO;
using Application.Features.Commands.rating.DeleteRating;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingConfiguration
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserRegistrationDTO>().ReverseMap();
            CreateMap<Rating, DeleteRatingCommand>().ReverseMap();
        }
    }
}
