using Database.Repositories;
using EnrolleeGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrolleeGuide.Stores
{
    public class ProgramsStore : IStore<ProgramModel>
    {
        private readonly ProgramRepository _repository;

        public ProgramsStore(ProgramRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ProgramModel> GetAsync(ProgramModel itemModel)
        {
            return ProgramModel.GetFromDomain(await _repository.GetAsync(itemModel.Id));
        }

        public async Task<ICollection<ProgramModel>> GetAllAsync()
        {
            var programs = await _repository.GetAllAsync();
            
            return programs.Select(ProgramModel.GetFromDomain)
                .ToList();
        }

        public async Task<ICollection<ProgramModel>> GetByUniversityAsync(UniversityModel universityModel)
        {
            var programs = await _repository.GetByUniversityAsync(universityModel.Id);

            return programs.Select(ProgramModel.GetFromDomain)
                .ToList();
        }

        public async Task SaveAsync(ProgramModel itemModel)
        {
            await _repository.SaveAsync(itemModel.ToDomain());
        }

        public async Task DeleteAsync(ProgramModel itemModel)
        {
            await _repository.DeleteAsync(itemModel.Id);
        }
    }
}
