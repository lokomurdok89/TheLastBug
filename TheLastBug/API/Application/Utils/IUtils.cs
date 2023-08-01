
using Domain;

namespace Application.Utils
{
    public interface IUtils
    {
        Task<Pais> AddPaisAsync(Pais pais);
        Task<Pais> UpdatePaisAsync(Pais pais, int id);
        Task DeletePaisAsync(int id);
        Task<List<Pais>> ListPaisAsync();
        Task<Pais> DetailPaisAsync(int id);
    }
}