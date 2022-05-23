using RifaCasinoAPI.DTOs;

namespace RifaCasinoAPI.Entidades
{
    public class WinnerCard
    {
        public string email { get; set; }
        public int idRifa { get; set; }
        public string nombreRifa { get; set; }
        public Tarjeta winnerCard { get; set; }
        public GetPremioDTO premio { get; set; }

        public WinnerCard( string email, int idRifa, string nombreRifa, Tarjeta winnercard, GetPremioDTO premio)
        {
            this.email = email;
            this.idRifa = idRifa;
            this.nombreRifa = nombreRifa;
            this.winnerCard = winnercard;
            this.premio = premio;
        }
    }
}
