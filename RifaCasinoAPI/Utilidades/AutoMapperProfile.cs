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
            //PARTICIPANTES
            CreateMap<IdentityUser, ParticipanteCreacionDTO > ()
                .ForMember(dto => dto.email, 
                    opciones => opciones.MapFrom(identityUser => identityUser.Email))
                .ForMember(dto => dto.idUser, 
                    opciones => opciones.MapFrom(identityUser => identityUser.Id))
                .ForMember(dto => dto.user,
                    opciones => opciones.MapFrom(identityUser => identityUser));
            CreateMap<ParticipanteCreacionDTO, Participantes>(); //lo que no se mapea es la lista de participaciones,
                                                                 //todavía no se puede llenar así que se deja para otro endpoint
            //RIFAS
            CreateMap<RifaCreacionDTO, RifaDTO>();
            CreateMap<RifaDTO, Rifa>();
            CreateMap<Rifa, GetRifaDTO>()
                .ForMember(getrifaDTO => getrifaDTO.premioList, opciones => opciones.MapFrom(RifaTOGetRifaDTO))
                .ForMember(getrifaDTO => getrifaDTO.participaciones, opciones => opciones.MapFrom(RifaTOGetRifaDTOParticipaciones)); 

            CreateMap<Rifa, PatchRifaDTO>().ReverseMap();
            CreateMap<EditarRifaDTO, Rifa>();
                //PREMIOS
            CreateMap<PremioCreacionDTO, PremioDTO>();
            CreateMap<PremioDTO, Premio>();
                //PARTICIPACIONES
            CreateMap<ParticipacionesCreacionDTO, ParticipacionesDTO>();
            CreateMap<ParticipacionesDTO, Participaciones>().ReverseMap();
            CreateMap<Participaciones, GetParticipacionesDTO>()
                .ForMember(GPDTO => GPDTO.id, opciones => opciones.MapFrom(Part => Part.id))
                .ForMember(GPDTO => GPDTO.idParticipante, opciones => opciones.MapFrom(Part => Part.id));

        }
        //TOKEN DE 1 AÑO: lore@gmail.com; aA123!   ADMIN 
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxvcmVAZ21haWwuY29tIiwiQWRtaW4iOiJUcnVlIiwiZXhwIjoxNjg0NzQ1OTM3fQ.L4pfzQjZ8HqeQYNHC-9Wp9cag4kUl6W6aC2Qqh8IHdw

        //TOKEN DE 1 AÑO: lorelore@gmail.com; aA123! 
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImxvcmVsb3JlQGdtYWlsLmNvbSIsImV4cCI6MTY4NDc2NjU2NX0.zynmGNUzzWzvj-yS3ahSg8co-rMcIsPBCX79Ykffwfo

        ////MAPEOS RIFA CONTROLLER////MAPEOS RIFA CONTROLLER////MAPEOS RIFA CONTROLLER////MAPEOS RIFA CONTROLLER
        //GET RIFA-----------------------------------------------------------------
        private List<GetPremioDTO> RifaTOGetRifaDTO(Rifa rifa, GetRifaDTO getRifaDTO)
        {
            var ListaGetPDTO = new List<GetPremioDTO>();
            if (rifa.premioList == null) return ListaGetPDTO;
            foreach (Premio premio in rifa.premioList)
            {
                // se intenta mapear de premio eventualmente a GetPremioDTO
                var premioDTO = premioToGetPremioDTO(premio);
                ListaGetPDTO.Add(premioDTO);
            }
            
            return ListaGetPDTO;
        }

        //funcion que regresa todo el objeto, no solo una propiedad
        private GetPremioDTO premioToGetPremioDTO(Premio premio)
        {
            var getPremioDTO = new GetPremioDTO();
            getPremioDTO.descripcion = premio.descripcion;
            getPremioDTO.nombre = premio.nombre;
            getPremioDTO.disponible = premio.disponible;

            return getPremioDTO;
        }
        ////MAPEOS PARTICIPACIONES CONTROLLER////MAPEOS PARTICIPACIONES CONTROLLER////MAPEOS PARTICIPACIONES CONTROLLER
        //POST PARTICIPACION
        private List<GetParticipacionesDTO> RifaTOGetRifaDTOParticipaciones(Rifa rifa, GetRifaDTO getRifaDTO)
        {
            var ListaGetParticipacionesDTO = new List<GetParticipacionesDTO>();
            if (rifa.participaciones == null) return ListaGetParticipacionesDTO;
            foreach (Participaciones participacion in rifa.participaciones)
            {
                // se intenta mapear cada participacion a GetParticipacionesDTO
                var GetparticipacionDTO = participacionTOGetParticipacionesDTO(participacion);
                ListaGetParticipacionesDTO.Add(GetparticipacionDTO);
            }
            return ListaGetParticipacionesDTO;
        }

        //funcion que regresa todo el objeto, no solo una propiedad
        private GetParticipacionesDTO participacionTOGetParticipacionesDTO(Participaciones participacion)
        {
            var getParticipacionesDTO = new GetParticipacionesDTO();

            getParticipacionesDTO.id = participacion.id;
            getParticipacionesDTO.idParticipante =  participacion.idParticipante;
            getParticipacionesDTO.idRifa = participacion.idRifa;
            getParticipacionesDTO.noLoteria = participacion.noLoteria;
            getParticipacionesDTO.ganador = participacion.ganador;
            return getParticipacionesDTO;
        }
    }
}