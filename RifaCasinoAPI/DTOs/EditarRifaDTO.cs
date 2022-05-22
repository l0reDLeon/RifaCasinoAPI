using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class EditarRifaDTO
    {

        
        public int id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} solo puede tener hasta 100 caracteres.")]
        public string? nombre { get; set; }
        public bool? vigente { get; set; }
    }
}
