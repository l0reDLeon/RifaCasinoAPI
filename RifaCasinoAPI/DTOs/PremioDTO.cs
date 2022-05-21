using RifaCasinoAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class PremioDTO
    {
        [Required]
        public int idRifa { get; set; }
        public Rifa rifa { get; set; }
        [Required]
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool disponible { get; set; }
    }
}
