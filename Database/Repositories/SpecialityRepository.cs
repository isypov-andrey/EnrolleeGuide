using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class SpecialityRepository
    {
        private readonly DataContext _readContext;

        public SpecialityRepository(DataContext dataContext)
        {
            _readContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<Speciality> GetAsync(int id)
        {
            return await _readContext.Specialities
                .AsNoTracking()
                .FirstOrDefaultAsync(speciality => speciality.Id == id);
        }

        public async Task<ICollection<Speciality>> GetAllAsync()
        {
            return await _readContext.Specialities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveAsync(Speciality speciality)
        {
            using (var editContext = new DataContext())
            {
                editContext.Entry(speciality)
                    .State = speciality.Id == 0
                    ? EntityState.Added
                    : EntityState.Modified;

                await editContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var editContext = new DataContext())
            {
                editContext.Specialities.RemoveRange(editContext.Specialities.Where(speciality => speciality.Id == id));

                await editContext.SaveChangesAsync();
            }
        }
    }
}
