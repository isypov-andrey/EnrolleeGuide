using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class CityRepository
    {
        private readonly DataContext _readContext;

        public CityRepository(DataContext dataContext)
        {
            _readContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<City> GetAsync(int id)
        {
            return await _readContext.Cities
                .AsNoTracking()
                .FirstOrDefaultAsync(city => city.Id == id);
        }

        public async Task<ICollection<City>> GetAllAsync()
        {
            return await _readContext.Cities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveAsync(City city)
        {
            using (var editContext = new DataContext())
            {
                editContext.Entry(city)
                    .State = city.Id == 0
                    ? EntityState.Added
                    : EntityState.Modified;

                await editContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var editContext = new DataContext())
            {
                editContext.Cities.RemoveRange(editContext.Cities.Where(city => city.Id == id));

                await editContext.SaveChangesAsync();
            }
        }
    }
}
