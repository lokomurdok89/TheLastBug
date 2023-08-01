
using System.Collections.Generic;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Usuarios.Any() || context.Paises.Any() || context.Regiones.Any() || context.Comunas.Any() || context.AyudasSociales.Any() || context.AsignacionesAyudasSociales.Any())
            {
                // La base de datos ya tiene datos, no es necesario agregar más.
                return;
            }
            
           context.Usuarios.AddRange(
                new Usuario { Nombre = "Administrador", CorreoElectronico = "admin@example.com", Contraseña = "hashed_password", Rol = "administrador" },
                new Usuario { Nombre = "Usuario Normal", CorreoElectronico = "user@example.com", Contraseña = "hashed_password", Rol = "usuario" }
            );

            // Agregar datos de muestra para Países
            var region1 = new Region { NombreRegion = "Región 1", PaisID = 1 };
            var region2 = new Region { NombreRegion = "Región 2", PaisID = 1 };
            var region3 = new Region { NombreRegion = "Región 3", PaisID = 2 };

            context.Paises.AddRange(
                new Pais { NombrePais = "País 1", Regiones = new List<Region>{region1,region2}  },
                new Pais { NombrePais = "País 2", Regiones = new List<Region>{region3} }
            );            

            // Agregar datos de muestra para Regiones
             context.Regiones.AddRange(region1, region2, region3);

            // Agregar datos de muestra para Comunas
            context.Comunas.AddRange(
                new Comuna { NombreComuna = "Comuna 1", RegionID = 1 },
                new Comuna { NombreComuna = "Comuna 2", RegionID = 1 },
                new Comuna { NombreComuna = "Comuna 3", RegionID = 2 },
                new Comuna { NombreComuna = "Comuna 4", RegionID = 3 }
            );

            // Agregar datos de muestra para Ayudas Sociales
            context.AyudasSociales.AddRange(
                new AyudaSocial { NombreAyudaSocial = "Ayuda Social 1", Descripcion = "Descripción de Ayuda Social 1" },
                new AyudaSocial { NombreAyudaSocial = "Ayuda Social 2", Descripcion = "Descripción de Ayuda Social 2" }
            );

            // Agregar datos de muestra para Asignaciones de Ayudas Sociales
          /* context.AsignacionesAyudasSociales.AddRange(
                new AsignacionAyudaSocial { UserID = 2, ComunaID = 1, AyudaSocialID = 1, Año = 2023, FechaAsignacion = DateTime.Now },
                new AsignacionAyudaSocial { UserID = 2, ComunaID = 3, AyudaSocialID = 2, Año = 2023, FechaAsignacion = DateTime.Now }
            );*/

           // await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}