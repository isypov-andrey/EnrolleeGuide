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

        public async Task<ICollection<CityModel>> GetAllAsync()
        {
            var cities = await _repository.GetAllAsync();

            return cities.Select(
                    city => new CityModel
                    {
                        Id = city.Id,
                        Name = city.Name
                    }
                )
                .ToList();
        }

        public async Task SaveAsync(CityModel itemModel)
        {
            var item = new City
            {
                Id = itemModel.Id,
                Name = itemModel.Name
            };
            await _repository.SaveAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
