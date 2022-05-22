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
                    opciones => opciones.MapFrom(emailUserTOstring))
                .ForMember(dto => dto.idUser, 
                    opciones => opciones.MapFrom(idUserTOstring))
                .ForMember(dto => dto.user,
                    opciones => opciones.MapFrom(UserTOUser));

            CreateMap<ParticipanteCreacionDTO, Participantes>(); //lo que no se mapea es la lista de participaciones,
                                                                 //todavía no se puede llenar así que se deja para otro endpoint
            CreateMap<ParticipanteCreacionDTO, Participantes>();
        }
        ////MAPEOS LOGIN USUARIOS////MAPEOS LOGIN USUARIOS////MAPEOS LOGIN USUARIOS////MAPEOS LOGIN USUARIOS
        //-----------------------------------------------------------------
        //Mapear desde IdentityUser a ParticipanteCreacionDTO
        private string emailUserTOstring(
            IdentityUser identityUser, ParticipanteCreacionDTO participanteCreacionDTO)
        { 
            return identityUser.Email;
        }
        private string idUserTOstring(
            IdentityUser identityUser, ParticipanteCreacionDTO participanteCreacionDTO)
        {

            return identityUser.Id;
        }
        private IdentityUser UserTOUser(
            IdentityUser identityUser, ParticipanteCreacionDTO participanteCreacionDTO)
        {
            return identityUser;
        }
        //-----------------------------------------------------------------

        ////MAPEOS RIFA CONTROLLER////MAPEOS RIFA CONTROLLER////MAPEOS RIFA CONTROLLER////MAPEOS RIFA CONTROLLER

    }
}