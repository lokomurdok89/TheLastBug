using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
   public class AyudaSocial
    {   [Key]
        public int AyudaSocialID { get; set; }
        public string NombreAyudaSocial { get; set; }
        public string Descripcion { get; set; }
    }
}