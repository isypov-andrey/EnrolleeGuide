using EnrolleeGuide.Models;
using EnrolleeGuide.Stores;

namespace EnrolleeGuide.ViewModels
{
    public class CityItemsViewModel : ItemsViewModelBase<CityModel>
    {
        public CityItemsViewModel(CitiesStore store) : base("Города", store)
        {
        }

        protected override string DeleteConfirmationMessage(CityModel item) => $"Вы уверены, что хотите удалить город '{item.Name}'?";
    }
}
