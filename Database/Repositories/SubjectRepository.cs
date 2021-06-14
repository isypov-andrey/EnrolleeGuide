using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class SubjectRepository
    {
        private readonly DataContext _readContext;

        public SubjectRepository(DataContext dataContext)
        {
            _readContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<Subject> GetAsync(int id)
        {
            return await _readContext.Subjects
                .AsNoTracking()
                .FirstOrDefaultAsync(subject => subject.Id == id);
        }

        public async Task<ICollection<Subject>> GetAllAsync()
        {
            return await _readContext.Subjects
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveAsync(Subject subject)
        {
            using (var editContext = new DataContext())
            {
                editContext.Entry(subject)
                    .State = subject.Id == 0
                    ? EntityState.Added
                    : EntityState.Modified;

                await editContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var editContext = new DataContext())
            {
                editContext.Subjects.RemoveRange(editContext.Subjects.Where(subject => subject.Id == id));

                await editContext.SaveChangesAsync();
            }
        }
    }
}
