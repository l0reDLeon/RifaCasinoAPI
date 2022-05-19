using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class RifaCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} solo puede tener hasta 100 caracteres.")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El campo {1} es requerido")]
        public bool vigente { get; set; }

    }
}
