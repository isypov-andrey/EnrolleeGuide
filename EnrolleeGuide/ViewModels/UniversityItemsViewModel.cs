using EnrolleeGuide.Models;
using EnrolleeGuide.Stores;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EnrolleeGuide.ViewModels
{
    public class UniversityItemsViewModel : ItemsViewModelBase<UniversityModel>
    {
        public readonly CitiesStore _citiesStore;

        private ObservableCollection<CityModel> _cities;

        public ObservableCollection<CityModel> Cities
        {
            get { return _cities; }
            set { SetProperty(ref _cities, value); }
        }

        public UniversityItemsViewModel(UniversitiesStore universitiesStore, CitiesStore citiesStore) : base("Университеты", universitiesStore)
        {
            _citiesStore = citiesStore;
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
    }
}
