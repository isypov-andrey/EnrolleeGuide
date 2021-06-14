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
    public class SubjectsStore : IStore<SubjectModel>
    {
        private readonly SubjectRepository _repository;

        public SubjectsStore(SubjectRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<SubjectModel> GetAsync(SubjectModel itemModel)
        {
            return SubjectModel.GetFromDomain(await _repository.GetAsync(itemModel.Id));
        }

        public async Task<ICollection<SubjectModel>> GetAllAsync()
        {
            var subjects = await _repository.GetAllAsync();

            return subjects.Select(SubjectModel.GetFromDomain)
                .ToList();
        }


        public async Task SaveAsync(SubjectModel itemModel)
        {
            await _repository.SaveAsync(itemModel.ToDomain());
        }

        public async Task DeleteAsync(SubjectModel itemModel)
        {
            await _repository.DeleteAsync(itemModel.Id);
        }
    }
}
