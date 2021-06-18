using EnrolleeGuide.Models;
using EnrolleeGuide.Stores;
using Entities;
using Prism.Commands;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EnrolleeGuide.ViewModels
{
    public class ProgramItemsViewModel : ItemsViewModelBase<ProgramModel>
    {
        private readonly IRegionManager _regionManager;

        private readonly SpecialitiesStore _specialitiesStore;

        private readonly SubjectsStore _subjectsStore;

        protected ProgramsStore ProgramsStore => (ProgramsStore)_store;

        public DelegateCommand BackToUniversitiesCommand { get; private set; }

        private ObservableCollection<TrainingFormTypeRecord> _trainingFormsTypes;

        public ObservableCollection<TrainingFormTypeRecord> TrainingFormsTypes
        {
            get { return _trainingFormsTypes; }
            set { SetProperty(ref _trainingFormsTypes, value); }
        }

        private ObservableCollection<SpecialityModel> _specialities;

        public ObservableCollection<SpecialityModel> Specialities
        {
            get { return _specialities; }
            set { SetProperty(ref _specialities, value); }
        }

        private ObservableCollection<SubjectForSelection> _subjects;

        public ObservableCollection<SubjectForSelection> Subjects
        {
            get { return _subjects; }
            private set { SetProperty(ref _subjects, value); }
        }

        private UniversityModel _university;

        public UniversityModel University
        {
            get { return _university; }
            set { SetProperty(ref _university, value); }
        }

        public ProgramItemsViewModel(IRegionManager regionManager, ProgramsStore programsStore, SpecialitiesStore specialitiesStore, SubjectsStore subjectsStore) : base("Программы", programsStore)
        {
            _regionManager = regionManager;
            _specialitiesStore = specialitiesStore;
            _subjectsStore = subjectsStore;

            BackToUniversitiesCommand = new DelegateCommand(NavigateToUniversities);

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SelectedItem) && SelectedItem != null)
                {
                    var selectedSubjects = new HashSet<int>(SelectedItem.EgeSubjects?.Select(subject => subject.Id) ?? new int[0]);
                    foreach (var subject in Subjects)
                    {
                        subject.Checked = selectedSubjects.Contains(subject.Id);
                    }
                }
            };
        }

        protected override async Task OnNavigatedToImpl(NavigationContext navigationContext)
        {
            University = navigationContext.Parameters.GetValue<UniversityModel>("university");

            await base.OnNavigatedToImpl(navigationContext);

            await LoadDataAsync();

            TrainingFormsTypes = new ObservableCollection<TrainingFormTypeRecord>
            {
                new TrainingFormTypeRecord
                {
                    Text = "Очная",
                    Value = TrainingFormType.FullTime
                },
                new TrainingFormTypeRecord
                {
                    Text = "Очно-заочная",
                    Value = TrainingFormType.PartTime
                },
                new TrainingFormTypeRecord
                {
                    Text = "Заочная",
                    Value = TrainingFormType.Extramural
                },
                new TrainingFormTypeRecord
                {
                    Text = "Дистанционная",
                    Value = TrainingFormType.Distance
                }
            };
        }

        private async Task LoadDataAsync()
        {
            var specialities = await _specialitiesStore.GetAllAsync();
            Specialities = new ObservableCollection<SpecialityModel>(specialities);
        }

        protected override void Create()
        {
            SelectedItem = new ProgramModel
            {
                UniversityId = University.Id
            };
        }

        protected override async Task LoadItemsAsync()
        {
            var items = await ProgramsStore.GetByUniversityAsync(University);
            Items = new ObservableCollection<ProgramModel>(items);

            var subjects = await _subjectsStore.GetAllAsync();
            Subjects = new ObservableCollection<SubjectForSelection>(
                subjects.Select(
                    subject => new SubjectForSelection
                    {
                        Id = subject.Id,
                        Name = subject.Name,
                        Checked = false
                    }
                )
            );
        }

        protected override void BeforeSave(ProgramModel item)
        {
            item.EgeSubjects = new ObservableCollection<SubjectModel>(
                    Subjects.Where(subject => subject.Checked)
                    .OfType<SubjectModel>()
                );
        }

        protected override string DeleteConfirmationMessage(ProgramModel item) => $"Вы уверены, что хотите удалить программу '{item.Name}'?";

        private void NavigateToUniversities()
        {
            _regionManager.RequestNavigate("ContentRegion", "UniversityItemsView");
        }
    }
}
