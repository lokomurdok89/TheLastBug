using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain;
public class Pais
{
    [BindNever] 
    public int PaisID { get; set; }
    public string NombrePais { get; set; }
    [BindNever] 
    public List<Region> Regiones { get; set; }
}