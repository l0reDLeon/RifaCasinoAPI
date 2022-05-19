using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.Entidades
{
    public class Premio
    {
        [Required]
        public int id { get; set; }
        public int idRifa { get; set;   }
        public Rifa rifa { get; set; }
        public string descripcion { get; set; }
        public bool disponible { get; set; }

    }
}
