using RifaCasinoAPI.Entidades;
using RifaCasinoAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class PatchRifaDTO
    {
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} solo puede tener hasta 100 caracteres.")]
        [PrimeraLetraMayuscula]
        public string? nombre { get; set; }
        public bool vigente { get; set; }
    }
}
