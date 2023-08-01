using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Persistence;

namespace Application.CRUD.CrudPais
{
    public class DetailsPais
    {
        private readonly DataContext _context;

        public DetailsPais(DataContext context)
        {
            _context = context;
           
        }
        public async Task<Pais> ListPaisAsync(int id){
            var result =  await _context.Paises.FindAsync(id); 
            if(result != null){
                return result;
            } 
            throw new Exception($"No existe Detalle Para el Id:{id}");          
        }
        
    }
}