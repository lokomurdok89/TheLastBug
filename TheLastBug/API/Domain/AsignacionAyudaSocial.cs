using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class AsignacionAyudaSocial
    {
        [Key]
        public int AsignacionID { get; set; }

    // Claves foráneas para las relaciones con Usuario, Comuna y AyudaSocial
        public int UserID { get; set; }
        public Usuario Usuario { get; set; }

        public int ComunaID { get; set; }
        public Comuna Comuna { get; set; }

        public int AyudaSocialID { get; set; }
        public AyudaSocial AyudaSocial { get; set; }

        public int Año { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}