using EnrolleeGuideCore.Models;
using Prism.Mvvm;
using Prism.Regions;

namespace EnrolleeGuideCore.ViewModels
{
    public class CityItemsViewModel : ItemsViewModelBase<CityModel>
    {

        private int _pageViews;
        public int PageViews
        {
            get { return _pageViews; }
            set { SetProperty(ref _pageViews, value); }
        }
    }
}
