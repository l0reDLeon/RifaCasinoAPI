using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.Entidades
{
    public class Premio
    {
        public int id { get; set; }
        [Required]
        public int idRifa { get; set;}
        public Rifa rifa { get; set; }
        [Required]
        [StringLength(75)]
        public string nombre { get; set; }
        [StringLength(75)]
        public string descripcion { get; set; }
        public bool disponible { get; set; }
    }
}
