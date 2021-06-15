using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class UniversityRepository
    {
        private readonly DataContext _readContext;

        public UniversityRepository(DataContext dataContext)
        {
            _readContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<University> GetAsync(int id)
        {
            return await _readContext.Universities
                .AsNoTracking()
                .FirstOrDefaultAsync(university => university.Id == id);
        }

        public async Task<ICollection<University>> GetAllAsync()
        {
            return await _readContext.Universities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveAsync(University university)
        {
            using (var editContext = new DataContext())
            {
                editContext.Entry(university)
                    .State = university.Id == 0
                    ? EntityState.Added
                    : EntityState.Modified;

                await editContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var editContext = new DataContext())
            {
                editContext.Universities.RemoveRange(editContext.Universities.Where(university => university.Id == id));

                await editContext.SaveChangesAsync();
            }
        }
    }
}
