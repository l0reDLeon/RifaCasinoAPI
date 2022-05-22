using RifaCasinoAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class GetParticipacionesDTO
    {        
        public string idParticipante { get; set; }
        public int idRifa { get; set; }
        public int noLoteria { get; set; }
        public bool ganador { get; set; }
    }
}
