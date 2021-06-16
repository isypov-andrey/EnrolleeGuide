using EnrolleeGuide.Models;
using EnrolleeGuide.Stores;
using Entities;
using Entities.Domain;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolleeGuide.ViewModels
{
    class UniversitiesMainViewModel : BindableBase, INavigationAware
    {
        private ICollection<CityModel> cities;
        public ICollection<CityModel> Cities
        {
            get { return cities; }
            private set { SetProperty(ref cities, value); }
        }

        private ICollection<SpecialityModel> specialities;
        public ICollection<SpecialityModel> Specialities
        {
            get { return specialities; }
            private set { SetProperty(ref specialities, value); }
        }

        private ICollection<SubjectForSelection> subjects;
        public ICollection<SubjectForSelection> Subjects
        {
            get { return subjects; }
            private set { SetProperty(ref subjects, value); }
        }

        private ICollection<ToggleListItem<TrainingFormType>> trainingForms;
        public ICollection<ToggleListItem<TrainingFormType>> TrainingForms
        {
            get { return trainingForms; }
            private set { SetProperty(ref trainingForms, value); }
        }


        private UniversityCriteria _criteria;
        public UniversityCriteria Criteria
        {
            get { return _criteria; }
            private set { SetProperty(ref _criteria, value); }
        }

        private ICollection<UniversityForList> _universities;
        public ICollection<UniversityForList> Universities
        {
            get { return _universities; }
            private set { SetProperty(ref _universities, value); }
        }

        public UniversityForList SelectedUniversity { get; set; }

        private ICollection<ProgramForList> _programs;
        public ICollection<ProgramForList> Programs
        {
            get { return _programs; }
            private set { SetProperty(ref _programs, value); }
        }

        private CitiesStore citiesStore;
        private SpecialitiesStore specialitiesStore;
        private SubjectsStore subjectsStore;
        private UniversitiesStore universitiesStore;

        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand UniversitySelectedCommand { get; private set; }

        public UniversitiesMainViewModel(CitiesStore citiesStore, SpecialitiesStore specialitiesStore, SubjectsStore subjectsStore, UniversitiesStore universitiesStore)
        {
            this.citiesStore = citiesStore;
            this.specialitiesStore = specialitiesStore;
            this.subjectsStore = subjectsStore;
            this.universitiesStore = universitiesStore;
            SearchCommand = new DelegateCommand(LoadDataAsync);
            UniversitySelectedCommand = new DelegateCommand(OnUniversitySelected);
            Criteria = new UniversityCriteria();
        }

        private void OnUniversitySelected()
        {
            if (SelectedUniversity == null)
            {
                return;
            }
            LoadProgramsAsync(SelectedUniversity.Id);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await LoadFilterDataAsync();
            LoadDataAsync();
        }

        private async Task LoadFilterDataAsync()
        {
            this.Cities = await citiesStore.GetAllAsync();
            this.Specialities = await specialitiesStore.GetAllAsync();
            var subjects = await subjectsStore.GetAllAsync();
            this.Subjects = subjects.Select(subject => new SubjectForSelection() { Id = subject.Id, Name = subject.Name, Checked = false }).ToList();
            this.TrainingForms = new ToggleListItem<TrainingFormType>[]
            {
                new ToggleListItem<TrainingFormType>(TrainingFormType.FullTime, "Очно"),
                new ToggleListItem<TrainingFormType>(TrainingFormType.Extramural, "Заочно"),
                new ToggleListItem<TrainingFormType>(TrainingFormType.FullTime, "Очно-заочно")
            };
        }

        private UniversityCriteria GetFilledCriteria()
        {
            Criteria.SubjectIds = Subjects.Where(subject => subject.Checked).Select(subject => subject.Id).ToList();
            Criteria.TrainingFormTypes = TrainingForms.Where(form => form.Checked).Select(form => form.Value).ToList();
            return Criteria;
        }

        private async void LoadDataAsync()
        {
            Universities = await universitiesStore.GetByCriteria(GetFilledCriteria());
        }

        private async void LoadProgramsAsync(int universityId)
        {
            Programs = await universitiesStore.GetProgramsByCriteria(universityId, GetFilledCriteria());
        }
    }
}
