using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Persistence;

namespace Application.CRUD.CrudPais
{
    public class EditPais
    {
        private readonly DataContext _context;

        public EditPais(DataContext context)
        {
            _context = context;
           
        }
        public async Task<Pais> EditPaisAsync(Pais pais, int id){
           
           if(_context.Paises.Any(p => p.NombrePais == pais.NombrePais && p.PaisID != id))throw new Exception($"Error Update Pais : {pais.NombrePais} Ya Existe");
           var _obj = _context.Paises.FirstOrDefault(x => x.PaisID == id) ;
            
            if(_obj != null){
                _obj.NombrePais = pais.NombrePais;
                await _context.SaveChangesAsync();
                return pais;
            }

            throw new Exception($"Error Update Pais : {pais.NombrePais} No Existe");
            
        }
        
    }
}