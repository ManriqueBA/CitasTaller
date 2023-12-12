using Microsoft.EntityFrameworkCore;
using Taller.Models;

namespace Taller.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opciones)
            : base(opciones)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mecanico> Mecanicos { get; set; }
        public DbSet<HorarioDisponible> HorariosDisponibles { get; set; }

        public DbSet<Citas> Citas { get; set; }
        public DbSet<Servicios> Servicios { get; set; }

    }
}
