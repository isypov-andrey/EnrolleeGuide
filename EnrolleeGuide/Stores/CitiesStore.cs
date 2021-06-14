using Database.Repositories;
using EnrolleeGuide.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolleeGuide.Stores
{
    public class CitiesStore : IStore<CityModel>
    {
        private readonly CityRepository _repository;

        public CitiesStore(CityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CityModel> GetAsync(CityModel itemModel)
        {
            return CityModel.GetFromDomain(await _repository.GetAsync(itemModel.Id));
        }

        public async Task<ICollection<CityModel>> GetAllAsync()
        {
            var cities = await _repository.GetAllAsync();

            return cities.Select(CityModel.GetFromDomain)
                .ToList();
        }

        public async Task SaveAsync(CityModel itemModel)
        {
            await _repository.SaveAsync(itemModel.ToDomain());
        }

        public async Task DeleteAsync(CityModel itemModel)
        {
            await _repository.DeleteAsync(itemModel.Id);
        }
    }
}
