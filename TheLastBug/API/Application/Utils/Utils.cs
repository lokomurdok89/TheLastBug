
using Application.CRUD.CrudPais;
using Domain;
using Persistence;

namespace Application.Utils
{
    public class Utils : IUtils
    {
        readonly CreatePais create;
        readonly DeletePais delete;
        readonly EditPais edit;
        readonly ListPais list;
        readonly DetailsPais detail;
        private readonly ILogger<Utils> _logger;

        public Utils(ILogger<Utils> logger,DataContext context)
        {
            create = new(context);
            edit = new(context);
            delete = new(context);
            list = new(context);
            detail = new(context);
            _logger = logger;
        }
        public async Task<Pais> AddPaisAsync(Pais pais)
        {
            _logger.LogInformation("Func: AddPaisAsync");
            return await create.AddPaisAsync(pais);
        }
        public async Task DeletePaisAsync(int id)
        {
            _logger.LogInformation("Func: DeletePaisAsync");
            await delete.DeletePaisAsync(id);
        }

        public async Task<Pais> DetailPaisAsync(int id)
        {
            _logger.LogInformation("Func: DetailPaisAsync");
            return await detail.ListPaisAsync(id);
        }

        public async Task<List<Pais>> ListPaisAsync()
        {
            _logger.LogInformation("Func: ListPaisAsync");
            return await list.ListPaisAsync();
        }
        public async Task<Pais> UpdatePaisAsync(Pais pais, int id)
        {
            _logger.LogInformation("Func: UpdatePaisAsync");
           return await edit.EditPaisAsync(pais, id);
        }
    }
}