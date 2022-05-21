using RifaCasinoAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.DTOs
{
    public class GetRifaDTO
    {
        public string nombre { get; set; }
        public bool vigente { get; set; }
        public List<Participaciones> particiones { get; set; }
        public List<Premio> premioList { get; set; }
    }
}
