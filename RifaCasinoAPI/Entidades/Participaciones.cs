using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.Entidades
{
    public class Participaciones
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
