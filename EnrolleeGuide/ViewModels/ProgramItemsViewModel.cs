using EnrolleeGuide.Models;
using EnrolleeGuide.Stores;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EnrolleeGuide.ViewModels
{
    public class ProgramItemsViewModel : ItemsViewModelBase<UniversityModel>
    {
        public readonly SpecialitiesStore _specialitiesStore;

        private ObservableCollection<SpecialityModel> _specialities;

        public ObservableCollection<SpecialityModel> Specialities
        {
            get { return _specialities; }
            set { SetProperty(ref _specialities, value); }
        }

        public ProgramItemsViewModel(UniversitiesStore universitiesStore, SpecialitiesStore specialitiesStore) : base("Программы", universitiesStore)
        {
            _specialitiesStore = specialitiesStore;
        }

        protected override async Task OnNavigatedToImpl(NavigationContext navigationContext)
        {
            await base.OnNavigatedToImpl(navigationContext);

            await LoadDataAsync();
        }

        protected override string DeleteConfirmationMessage(UniversityModel item) => $"Вы уверены, что хотите удалить программу '{item.Name}'?";

        private async Task LoadDataAsync()
        {
            var specialities = await _specialitiesStore.GetAllAsync();
            Specialities = new ObservableCollection<SpecialityModel>(specialities);
        }
    }
}
