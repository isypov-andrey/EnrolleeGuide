using Database.Repositories;
using EnrolleeGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrolleeGuide.Stores
{
    public class SpecialitiesStore : IStore<SpecialityModel>
    {
        private readonly SpecialityRepository _repository;

        public SpecialitiesStore(SpecialityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<SpecialityModel> GetAsync(SpecialityModel itemModel)
        {
            return SpecialityModel.GetFromDomain(await _repository.GetAsync(itemModel.Id));
        }

        public async Task<ICollection<SpecialityModel>> GetAllAsync()
        {
            var specialities = await _repository.GetAllAsync();

            return specialities.Select(SpecialityModel.GetFromDomain)
                .ToList();
        }

        public async Task SaveAsync(SpecialityModel itemModel)
        {
            await _repository.SaveAsync(itemModel.ToDomain());
        }

        public async Task DeleteAsync(SpecialityModel itemModel)
        {
            await _repository.DeleteAsync(itemModel.Id);
        }
    }
}
