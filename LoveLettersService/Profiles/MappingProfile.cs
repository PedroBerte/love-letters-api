using AutoMapper;
using LoveLetters.Repository.Context;
using LoveLetters.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Service.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Users, UsersDTO>();
            CreateMap<UsersDTO, Users>();

            CreateMap<Invites, InvitesDTO>();
            CreateMap<InvitesDTO, Invites>();
        }
    }
}
