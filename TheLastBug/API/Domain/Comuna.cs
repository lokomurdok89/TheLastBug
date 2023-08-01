using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Domain;
public class Comuna
{
    [BindNever]     
    public int ComunaID { get; set; }
    public string NombreComuna { get; set; }

    // Clave foránea para la relación con Region
    [BindNever] 
    public int RegionID { get; set; }
    [BindNever] 
    public Region Region { get; set; }
}