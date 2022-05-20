using RifaCasinoAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class ParticipacionesDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int idParticipante { get; set; }
        public Participantes participante { get; set; }
        [Required]
        public int idRifa { get; set; }
        public int noLoteria { get; set; }
        public int idPremio { get; set; }
        public bool ganador { get; set; }
    }
}
