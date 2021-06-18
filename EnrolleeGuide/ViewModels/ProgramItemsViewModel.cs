using EnrolleeGuide.Models;
using EnrolleeGuide.Stores;
using Entities;
using Prism.Commands;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EnrolleeGuide.ViewModels
{
    public class ProgramItemsViewModel : ItemsViewModelBase<ProgramModel>
    {
        private readonly IRegionManager _regionManager;

        private readonly SpecialitiesStore _specialitiesStore;

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

        private UniversityModel _university;

        public UniversityModel University
        {
            get { return _university; }
            set { SetProperty(ref _university, value); }
        }

        public ProgramItemsViewModel(IRegionManager regionManager, ProgramsStore programsStore, SpecialitiesStore specialitiesStore) : base("Программы", programsStore)
        {
            _regionManager = regionManager;
            _specialitiesStore = specialitiesStore;

            BackToUniversitiesCommand = new DelegateCommand(NavigateToUniversities);
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
        }

        protected override string DeleteConfirmationMessage(ProgramModel item) => $"Вы уверены, что хотите удалить программу '{item.Name}'?";

        private void NavigateToUniversities()
        {
            _regionManager.RequestNavigate("ContentRegion", "UniversityItemsView");
        }
    }
}
