using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.Entidades
{
    public class Participantes
    {
        [Required]
        public int id { get; set; }
        [EmailAddress]
        public string email { get; set; }        
        public string password { get; set; }
        public List<Participaciones> participacionesList { get; set; }
    }
}
