using Entities;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace EnrolleeGuide.Models
{
    public class ProgramModel : BindableBase
    {
        public int Id { get; set; }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// <see cref="Description"/>
        /// </summary>
        private string _description;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        /// <summary>
        /// <see cref="UniversityId"/>
        /// </summary>
        private int _universityId;

        /// <summary>
        /// Идентификатор университета
        /// </summary>
        public int UniversityId
        {
            get => _universityId;
            set => SetProperty(ref _universityId, value);
        }

        /// <summary>
        /// <see cref="SpecialityId"/>
        /// </summary>
        private int _specialityId;

        /// <summary>
        /// Идентификатор специальности
        /// </summary>
        public int SpecialityId
        {
            get => _specialityId;
            set => SetProperty(ref _specialityId, value);
        }

        /// <summary>
        /// <see cref="TrainingForms"/>
        /// </summary>
        private ObservableCollection<TrainingFormModel> _trainingForms = new ObservableCollection<TrainingFormModel>();

        /// <summary>
        /// Формы обучения
        /// </summary>
        public ObservableCollection<TrainingFormModel> TrainingForms
        {
            get => _trainingForms;
            set => SetProperty(ref _trainingForms, value);
        }

        /// <summary>
        /// <see cref="EgeSubjects"/>
        /// </summary>
        private ObservableCollection<SubjectModel> _egeSubjects = new ObservableCollection<SubjectModel>();

        /// <summary>
        /// Предметы ЕГЭ
        /// </summary>
        public ObservableCollection<SubjectModel> EgeSubjects
        {
            get => _egeSubjects;
            set => SetProperty(ref _egeSubjects, value);
        }

        public static ProgramModel GetFromDomain(Program program)
        {
            if (program == null)
            {
                return null;
            }

            var programModel = new ProgramModel
            {
                Id = program.Id,
                Name = program.Name,
                Description = program.Description,
                UniversityId = program.UniversityId,
                SpecialityId = program.SpecialityId
            };
            if (program.TrainingForms?.Any() == true)
            {
                programModel.TrainingForms.AddRange(
                    program.TrainingForms.Select(TrainingFormModel.GetFromDomain)
                );
            }
            if (program.EgeSubjects?.Any() == true)
            {
                programModel.EgeSubjects.AddRange(
                    program.EgeSubjects.Select(SubjectModel.GetFromDomain)
                );
            }

            return programModel;
        }

        public Program ToDomain()
        {
            return new Program
            {
                Id = Id,
                Name = Name,
                Description = Description,
                UniversityId = UniversityId,
                SpecialityId = SpecialityId,
                TrainingForms = TrainingForms.Select(form => form.ToDomain())
                    .ToList(),
                EgeSubjects = EgeSubjects.Select(subject => subject.ToDomain())
                    .ToList()
            };
        }
    }
}
