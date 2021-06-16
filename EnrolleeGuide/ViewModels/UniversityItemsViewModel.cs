using EnrolleeGuide.Models;
using EnrolleeGuide.Stores;
using Prism.Commands;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EnrolleeGuide.ViewModels
{
    public class UniversityItemsViewModel : ItemsViewModelBase<UniversityModel>
    {
        private readonly IRegionManager _regionManager;

        private readonly CitiesStore _citiesStore;

        private ObservableCollection<CityModel> _cities;

        public ObservableCollection<CityModel> Cities
        {
            get { return _cities; }
            set { SetProperty(ref _cities, value); }
        }

        public DelegateCommand<UniversityModel> OpenProgramsCommand { get; private set; }

        public UniversityItemsViewModel(IRegionManager regionManager, UniversitiesStore universitiesStore, CitiesStore citiesStore) : base("Университеты", universitiesStore)
        {
            _regionManager = regionManager;
            _citiesStore = citiesStore;

            OpenProgramsCommand = new DelegateCommand<UniversityModel>(university => OpenPrograms(university));
        }

        protected override async Task OnNavigatedToImpl(NavigationContext navigationContext)
        {
            await base.OnNavigatedToImpl(navigationContext);

            await LoadDataAsync();
        }

        protected override string DeleteConfirmationMessage(UniversityModel item) => $"Вы уверены, что хотите удалить университет '{item.Name}'?";

        private async Task LoadDataAsync()
        {
            var cities = await _citiesStore.GetAllAsync();
            Cities = new ObservableCollection<CityModel>(cities);
        }

        private void OpenPrograms(UniversityModel university)
        {
            if (university == null)
            {
                return;
            }

            var parameters = new NavigationParameters();
            parameters.Add("university", university);

            _regionManager.RequestNavigate("ContentRegion", "ProgramItemsView", parameters);
        }
    }
}
