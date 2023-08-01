using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Comuna> Comunas { get; set; }
        public DbSet<AyudaSocial> AyudasSociales { get; set; }
        public DbSet<AsignacionAyudaSocial> AsignacionesAyudasSociales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar las relaciones y restricciones entre entidades, si las hay.
            // Por ejemplo, configurar las claves primarias y foráneas.
            modelBuilder.Entity<AsignacionAyudaSocial>()
                .HasKey(a => a.AsignacionID);
                // Ejemplo de configuración de una relación uno a muchos entre Regiones y Comunas.
            modelBuilder.Entity<Pais>()
                .HasMany(p => p.Regiones)
                .WithOne(r => r.Pais)
                .HasForeignKey(r => r.PaisID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de relación uno a muchos entre Regiones y Comunas.
            modelBuilder.Entity<Region>()
                .HasMany(r => r.Comunas)
                .WithOne(c => c.Region)
                .HasForeignKey(c => c.RegionID)
                .OnDelete(DeleteBehavior.Cascade);           
        }

    }
}