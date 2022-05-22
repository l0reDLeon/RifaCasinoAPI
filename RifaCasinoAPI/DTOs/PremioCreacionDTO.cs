using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class PremioCreacionDTO
    {
        [Required]
        public int idRifa { get; set; }
        [Required]
        [StringLength(75)]
        public string nombre { get; set; }
        [Required]
        [StringLength(75)]
        public string descripcion { get; set; }
    }
}
