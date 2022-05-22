using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RifaCasinoAPI.DTOs;
using RifaCasinoAPI.Entidades;

namespace RifaCasinoAPI.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IdentityUser, ParticipanteCreacionDTO > ()
                .ForMember(dto => dto.email, 
                    opciones => opciones.MapFrom(identityUser => identityUser.Email))
                .ForMember(dto => dto.idUser, 
                    opciones => opciones.MapFrom(identityUser => identityUser.Id))
                .ForMember(dto => dto.user,
                    opciones => opciones.MapFrom(identityUser => identityUser));

            CreateMap<ParticipanteCreacionDTO, Participantes>(); //lo que no se mapea es la lista de participaciones,
                                                                 //todavía no se puede llenar así que se deja para otro endpoint
            CreateMap<RifaCreacionDTO, RifaDTO>();
            CreateMap<RifaDTO, Rifa>();
            CreateMap<Rifa, GetRifaDTO>();
            CreateMap<Rifa, PatchRifaDTO>().ReverseMap();

            CreateMap<PremioCreacionDTO, PremioDTO>();
            CreateMap<PremioDTO, Premio>();
        }
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxvcmVAZ21haWwuY29tIiwiQWRtaW4iOiJUcnVlIiwiZXhwIjoxNjg0NzQ1OTM3fQ.L4pfzQjZ8HqeQYNHC-9Wp9cag4kUl6W6aC2Qqh8IHdw
        ////MAPEOS RIFA CONTROLLER////MAPEOS RIFA CONTROLLER////MAPEOS RIFA CONTROLLER////MAPEOS RIFA CONTROLLER
        //POST RIFA-----------------------------------------------------------------

    }
}