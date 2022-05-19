using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class CredencialesUsuario
    {
        [Required]
        public string userName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }

        
        
    }
}
