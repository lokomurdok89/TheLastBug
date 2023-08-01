
using Domain;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.CRUD.CrudPais
{

    public class CreatePais
    {
        private readonly DataContext _context;

        public CreatePais(DataContext context)
        {
            _context = context;
           
        }
        public async Task<Pais> AddPaisAsync(Pais pais){

            var _obj = _context.Paises.FirstOrDefault(x => x.NombrePais.ToUpper().Equals(pais.NombrePais.ToUpper()));
            if(_obj == null){
                _context.Paises.Add(pais);
                await _context.SaveChangesAsync();
                return pais;
            }
            throw new Exception($"Error Insertar Pais : {_obj.NombrePais} ya existe");
            
        }        
    }
}