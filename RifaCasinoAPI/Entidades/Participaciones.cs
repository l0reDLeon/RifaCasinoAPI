using System.ComponentModel.DataAnnotations;

namespace RifaCasinoAPI.Entidades
{
    public class Participaciones:IValidatableObject
    {
        public int id { get; set; }
        [Required]
        public string idParticipante { get; set; }
        public Participantes participante { get; set; }
        [Required]
        public int idRifa { get; set; }
        public Rifa rifa { get; set; }
        public int noLoteria { get; set; }
        public bool ganador { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(noLoteria < 1 || noLoteria > 54) {
                yield return new ValidationResult("El número de lotería fuera de rango", new string[]
                {
                    nameof(noLoteria)
                });
            }
            throw new NotImplementedException();
        }
    }
}
