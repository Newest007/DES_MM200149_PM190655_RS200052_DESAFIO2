using Microsoft.EntityFrameworkCore;

namespace AplicacionAPI_Desafio2_MM200149_PM190655.Models
{
    public class ConexionContext : DbContext
    {
        public ConexionContext(DbContextOptions options) : base(options) { }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }

    }
}
