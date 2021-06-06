using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Abstractions.Repositories
{
    public interface IReadRepository
    {
        Task<ICollection<City>> GetCitiesAsync();
    }
}
