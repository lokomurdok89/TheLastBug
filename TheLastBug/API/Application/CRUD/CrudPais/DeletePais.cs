using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Persistence;

namespace Application.CRUD.CrudPais
{
    public class DeletePais
    {
        private readonly DataContext _context;

        public DeletePais(DataContext context)
        {
            _context = context;
           
        }
        public async Task DeletePaisAsync(int id){
            var _obj = await _context.Paises.FindAsync(id) ?? throw new Exception($"Error Delete Pais : {id} no existe");
            _context.Paises.Remove(_obj);
            await _context.SaveChangesAsync();
        }
        
    }
}