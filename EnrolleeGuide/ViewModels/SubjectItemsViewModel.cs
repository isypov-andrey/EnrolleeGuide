using EnrolleeGuide.Models;
using EnrolleeGuide.Stores;

namespace EnrolleeGuide.ViewModels
{
    public class SubjectItemsViewModel : ItemsViewModelBase<SubjectModel>
    {
        public SubjectItemsViewModel(SubjectsStore store) : base("Предметы", store)
        {
        }

        protected override string DeleteConfirmationMessage(SubjectModel item) => $"Вы уверены, что хотите удалить предмет '{item.Name}'?";
    }
}
