namespace RifaCasinoAPI.Entidades
{
    public class Tarjeta
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string refran { get; set; }

        public Tarjeta(int id, string nombre, string refran)
        {
            this.id=id;
            this.nombre = nombre;
            this.refran = refran;
        }
    }
}
