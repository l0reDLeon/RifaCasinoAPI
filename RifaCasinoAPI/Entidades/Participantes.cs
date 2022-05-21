using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.Entidades
{
    public class Participantes
    {
        public int id { get; set; }
        [EmailAddress]
        public string email { get; set; }        
        public string idUser { get; set; }
        public IdentityUser user { get; set; }
        public List<Participaciones> participacionesList { get; set; }
    }
}
