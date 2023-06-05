using Microsoft.EntityFrameworkCore;
using TaskManager.Entidades;

namespace TaskManager
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // Entidad: Tarea ~ Nombre de la tabla: Tareas
        public DbSet<Tarea> Tareas { get; set; }
    }
}
