using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Domain;
public class Region
{
     [BindNever]

    public int RegionID { get; set; }
    public string NombreRegion { get; set; }
    
    public int PaisID { get; set; }
   
    public Pais Pais { get; set; }
    [BindNever] 
    // Relación uno a muchos con Comunas
    public List<Comuna> Comunas { get; set; }
}