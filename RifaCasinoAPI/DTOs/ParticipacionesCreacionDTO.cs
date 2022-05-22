using RifaCasinoAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class ParticipacionesCreacionDTO
    {

        [Required]
        public int idRifa { get; set; }
        [Required]
        public int noLoteria { get; set; }
    }
}
