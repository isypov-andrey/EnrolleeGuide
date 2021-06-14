using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolleeGuideCore.ViewModels
{
    public abstract class ItemsViewModelBase<TItem> : BindableBase, INavigationAware
    {
        public ObservableCollection<TItem> Items { get; set; }

        private TItem _selectedItem;

        public TItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

        public ItemsViewModelBase()
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
