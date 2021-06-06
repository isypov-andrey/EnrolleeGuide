using Database.Abstractions.Repositories;

using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class ReadRepository : IReadRepository
    {
        private readonly DataContext _dataContext;

        public ReadRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<ICollection<City>> GetCitiesAsync()
        {
            return await _dataContext.Cities
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
