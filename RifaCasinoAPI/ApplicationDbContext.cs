using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RifaCasinoAPI.Entidades;

namespace RifaCasinoAPI
{
    public class ApplicationDbContext:IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Rifa> Rifas { get; set; }
        public DbSet<Premio> Premios { get; set; }
        public DbSet<Participantes> Participantes { get; set; }
        public DbSet<Participaciones> Participaciones { get; set; }

    }
}
