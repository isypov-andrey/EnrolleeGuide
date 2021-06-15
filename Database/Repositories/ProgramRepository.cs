using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class ProgramRepository
    {
        private readonly DataContext _readContext;

        public ProgramRepository(DataContext dataContext)
        {
            _readContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<Program> GetAsync(int id)
        {
            return await _readContext.Programs
                .AsNoTracking()
                .FirstOrDefaultAsync(program => program.Id == id);
        }

        public async Task<ICollection<Program>> GetAllAsync()
        {
            return await _readContext.Programs
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveAsync(Program program)
        {
            using (var editContext = new DataContext())
            {
                editContext.Entry(program)
                    .State = program.Id == 0
                    ? EntityState.Added
                    : EntityState.Modified;

                await editContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var editContext = new DataContext())
            {
                editContext.Programs.RemoveRange(editContext.Programs.Where(program => program.Id == id));

                await editContext.SaveChangesAsync();
            }
        }
    }
}
