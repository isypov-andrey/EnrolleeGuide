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

        public async Task<ICollection<Program>> GetByUniversityAsync(int universityId)
        {
            return await _readContext.Programs
                .AsNoTracking()
                .Where(program => program.UniversityId == universityId)
                .Include(program => program.TrainingForms)
                .Include(program => program.Speciality)
                .Include(program => program.EgeSubjects)
                .ToListAsync();
        }

        public async Task SaveAsync(Program changedProgram)
        {
            using (var editContext = new DataContext())
            {
                if (changedProgram.Id == 0)
                {
                    editContext.Programs.Add(changedProgram);
                }
                else
                {
                    var existsProgram = await editContext.Programs
                        .Include(program => program.EgeSubjects)
                        .Include(program => program.TrainingForms)
                        .FirstOrDefaultAsync(university => university.Id == changedProgram.Id);

                    existsProgram.Name = changedProgram.Name;
                    existsProgram.Description = changedProgram.Description;
                    existsProgram.SpecialityId = changedProgram.SpecialityId;

                    var oldEgeSubjects = existsProgram.EgeSubjects.ToDictionary(subject => subject.Id);
                    if (changedProgram.EgeSubjects?.Any() == true)
                    {
                        foreach (var newSubject in changedProgram.EgeSubjects)
                        {
                            if (oldEgeSubjects.TryGetValue(newSubject.Id, out var oldSubject))
                            {
                                oldEgeSubjects.Remove(newSubject.Id);
                            }
                            else
                            {
                                existsProgram.EgeSubjects.Add(newSubject);
                            }
                        }
                    }

                    foreach(var oldSubject in oldEgeSubjects.Values)
                    {
                        existsProgram.EgeSubjects.Remove(oldSubject);
                    }

                    var oldTrainingForms = existsProgram.TrainingForms.ToDictionary(trainingForm => trainingForm.Id);
                    if (changedProgram.TrainingForms?.Any() == true)
                    {
                        foreach (var newTrainingForm in changedProgram.TrainingForms)
                        {
                            if (oldTrainingForms.TryGetValue(newTrainingForm.Id, out var oldTrainingForm))
                            {
                                oldTrainingForms.Remove(newTrainingForm.Id);
                                oldTrainingForm.Type = newTrainingForm.Type;
                                oldTrainingForm.DurationInYears = newTrainingForm.DurationInYears;
                                oldTrainingForm.Price = newTrainingForm.Price;
                                oldTrainingForm.BudgetExamPoints = newTrainingForm.BudgetExamPoints;
                                oldTrainingForm.BudgetPlacesCount = newTrainingForm.BudgetPlacesCount;
                            }
                            else
                            {
                                existsProgram.TrainingForms.Add(newTrainingForm);
                            }
                        }
                    }

                    if (oldTrainingForms.Any())
                    {
                        editContext.TrainingForms.RemoveRange(oldTrainingForms.Values);
                    }
                }

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
