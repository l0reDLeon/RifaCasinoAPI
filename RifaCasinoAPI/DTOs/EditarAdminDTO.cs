using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class EditarAdminDTO
    {

        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}
