using Microsoft.EntityFrameworkCore;
using Orders.Shared.Entities;

namespace Orders.Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) //Esta linea realiza la conexion a la base de datos.
        {
        }

        public DbSet<Country> Countries { get; set; } //Conexion a base de datos y mapea a la tabla Countries.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique(); //Indica que el campo Name de la entidad Country es un índice único, lo que significa que no puede haber dos registros con el mismo valor en ese campo.
        }
    }
}