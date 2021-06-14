using EnrolleeGuide.Models;
using EnrolleeGuide.Stores;

namespace EnrolleeGuide.ViewModels
{
    public class SpecialityItemsViewModel : ItemsViewModelBase<SpecialityModel>
    {
        public SpecialityItemsViewModel(SpecialitiesStore store) : base("Специальности", store)
        {
        }

        protected override string DeleteConfirmationMessage(SpecialityModel item) => $"Вы уверены, что хотите удалить специальность '{item.Name}'?";
    }
}
