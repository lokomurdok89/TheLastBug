using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CRUD.CrudPais
{
    public class ListPais
    {
        private readonly DataContext _context;

        public ListPais(DataContext context)
        {
            _context = context;
           
        }
        public async Task<List<Pais>> ListPaisAsync(){

            return await _context.Paises.ToListAsync();
            
        }

        
    }
}