using Entities;
using Entities.Domain;
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
                .Include(university => university.Addresses)
                .FirstOrDefaultAsync(university => university.Id == id);
        }

        public async Task<ICollection<University>> GetAllAsync()
        {
            return await _readContext.Universities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveAsync(University changedUniversity)
        {
            using (var editContext = new DataContext())
            {
                if (changedUniversity.Id == 0)
                {
                    editContext.Universities.Add(changedUniversity);
                }
                else
                {
                    var existsUniversity = await editContext.Universities
                        .Include(university => university.Addresses)
                        .FirstOrDefaultAsync(university => university.Id == changedUniversity.Id);

                    existsUniversity.Name = changedUniversity.Name;
                    existsUniversity.Description = changedUniversity.Description;
                    existsUniversity.CityId = changedUniversity.CityId;

                    var oldAddresses = existsUniversity.Addresses.ToDictionary(address => address.Id);
                    if (changedUniversity.Addresses?.Any() == true)
                    {
                        foreach(var newAddress in changedUniversity.Addresses)
                        {
                            if (oldAddresses.TryGetValue(newAddress.Id, out var oldAddress))
                            {
                                oldAddresses.Remove(newAddress.Id);
                                oldAddress.FullAddress = newAddress.FullAddress;
                                oldAddress.Phone = newAddress.Phone;
                            }
                            else
                            {
                                existsUniversity.Addresses.Add(newAddress);
                            }
                        }
                    }

                    if (oldAddresses.Any())
                    {
                        editContext.Addresses.RemoveRange(oldAddresses.Values);
                    }
                }

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

        public async Task<ICollection<UniversityForList>> GetForList(string query, int? cityId, int? specialityId, ICollection<int> subjectIds, ICollection<TrainingFormType> trainingFormTypes)
        {
            IQueryable<TrainingForm> req = _readContext.TrainingForms
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.Trim();
                req = req.Where(trainingForm => trainingForm.Program.Name.Contains(query) || trainingForm.Program.University.Name.Contains(query));
            }
            if (cityId != null)
            {
                req = req.Where(trainingForm => trainingForm.Program.University.CityId == cityId);
            }
            if (specialityId != null)
            {
                req = req.Where(trainingForm => trainingForm.Program.SpecialityId == specialityId);
            }
            if (subjectIds != null && subjectIds.Count > 0)
            {
                req =  req.Where(trainingForm => trainingForm.Program.EgeSubjects.Any(subject => subjectIds.Contains(subject.Id)));
            }
            if (trainingFormTypes != null && trainingFormTypes.Count > 0)
            {
                req = req.Where(trainingForm => trainingFormTypes.Contains(trainingForm.Type));
            }

            var requestQuery = req.GroupBy(trainingForm => trainingForm.Program.University)
                .Select(
                    q => new UniversityForList
                    {
                        Id = q.Key.Id,
                        Name = q.Key.Name,
                        Description = q.Key.Description,
                        BudgetPlacesCount = q.Sum(trainingForm => trainingForm.BudgetPlacesCount),
                        PriceFrom = q.Min(trainingForm => trainingForm.Price),
                        BudgetExamPointsFrom = q.Min(trainingForm => trainingForm.BudgetExamPoints),
                        ProgramsCount = q.Select(trainingForm => trainingForm.Program.Id).Distinct().Count()
                    }
                );

            return await requestQuery.ToListAsync();
        }

        public async Task<ICollection<ProgramForList>> GetProgramsForList(int universityId, string query, int? cityId, int? specialityId, ICollection<int> subjectIds, ICollection<TrainingFormType> trainingFormTypes)
        {
            IQueryable<TrainingForm> req = _readContext.TrainingForms
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = query.Trim();
                req = req.Where(trainingForm => trainingForm.Program.Name.Contains(query) || trainingForm.Program.University.Name.Contains(query));
            }
            if (cityId != null)
            {
                req = req.Where(trainingForm => trainingForm.Program.University.CityId == cityId);
            }
            if (specialityId != null)
            {
                req = req.Where(trainingForm => trainingForm.Program.SpecialityId == specialityId);
            }
            if (subjectIds != null && subjectIds.Count > 0)
            {
                req = req.Where(trainingForm => trainingForm.Program.EgeSubjects.Any(subject => subjectIds.Contains(subject.Id)));
            }
            if (trainingFormTypes != null && trainingFormTypes.Count > 0)
            {
                req = req.Where(trainingForm => trainingFormTypes.Contains(trainingForm.Type));
            }

            return await req.Where(trainingForm => trainingForm.Program.UniversityId == universityId)
                .GroupBy(trainingForm => trainingForm.Program)
                .Select(
                    q => new ProgramForList
                    {
                        Id = q.Key.Id,
                        Name = q.Key.Name,
                        Description = q.Key.Description,
                        Subjects = q.Key.EgeSubjects.Select(egeSubject => egeSubject.Name),
                        TrainingForms = q.Select(trainingForm => trainingForm.Type),
                        BudgetPlacesCount = q.Sum(trainingForm => trainingForm.BudgetPlacesCount),
                        Price = q.Max(trainingForm => trainingForm.Price),
                        BudgetExamPoints = q.Max(trainingForm => trainingForm.BudgetExamPoints),
                        DurationInYears = q.Max(trainingForm => trainingForm.DurationInYears)
                    }
                ).ToListAsync();
        }
    }
}
