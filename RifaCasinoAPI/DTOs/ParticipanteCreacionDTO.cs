using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class ParticipanteCreacionDTO
    {
        [EmailAddress]
        public string email { get; set; }
        public string idUser { get; set; }
        public IdentityUser user { get; set; }
    }
}
