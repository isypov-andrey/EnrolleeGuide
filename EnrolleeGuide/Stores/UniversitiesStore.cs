using Database.Repositories;
using EnrolleeGuide.Models;
using Entities;
using Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolleeGuide.Stores
{
    public class UniversitiesStore : IStore<UniversityModel>
    {
        private readonly UniversityRepository _repository;

        public UniversitiesStore(UniversityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<UniversityModel> GetAsync(UniversityModel itemModel)
        {
            return UniversityModel.GetFromDomain(await _repository.GetAsync(itemModel.Id));
        }

        public async Task<ICollection<UniversityModel>> GetAllAsync()
        {
            var universities = await _repository.GetAllAsync();

            return universities.Select(UniversityModel.GetFromDomain)
                .ToList();
        }

        public async Task SaveAsync(UniversityModel itemModel)
        {
            await _repository.SaveAsync(itemModel.ToDomain());
        }

        public async Task DeleteAsync(UniversityModel itemModel)
        {
            await _repository.DeleteAsync(itemModel.Id);
        }

        public async Task<ICollection<UniversityForList>> GetByCriteria(UniversityCriteria criteria)
        {
            return await _repository.GetForList(criteria.Query, criteria.CityId, criteria.SpecialityId, criteria.SubjectIds, criteria.TrainingFormTypes);
        }
    }
}
