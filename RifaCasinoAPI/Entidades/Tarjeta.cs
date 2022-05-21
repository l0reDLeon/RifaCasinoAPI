namespace RifaCasinoAPI.Entidades
{
    public class Tarjeta
    {
        int id { get; set; }
        string nombre { get; set; }
        string refran { get; set; }

        public Tarjeta(int id, string nombre, string refran)
        {
            this.id=id;
            this.nombre = nombre;
            this.refran = refran;
        }
    }
}
