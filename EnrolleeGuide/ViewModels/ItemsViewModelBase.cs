using EnrolleeGuide.Stores;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EnrolleeGuide.ViewModels
{
    public abstract class ItemsViewModelBase<TItem> : BindableBase, INavigationAware
    {
        private readonly IStore<TItem> _store;


        private string _title;

        public string Title
        {
            get { return _title; }
            private set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<TItem> _items;

        public ObservableCollection<TItem> Items
        {
            get
            {
                return _items;
            }
            set
            {
                SetProperty(ref _items, value);
            }
        }

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

        public DelegateCommand<int?> DeleteCommand { get; private set; }

        public ItemsViewModelBase(string title, IStore<TItem> store)
        {
            Title = title;
            _store = store;

            DeleteCommand = new DelegateCommand<int?>(async id => await DeleteAsync(id));
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await LoadDataAsync();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async Task SaveAsync(TItem item)
        {
            await _store.SaveAsync(item);
        }

        private async Task LoadDataAsync()
        {
            var items = await _store.GetAllAsync();

            Items = new ObservableCollection<TItem>(items);
        }

        private async Task DeleteAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _store.DeleteAsync(id.Value);

            await LoadDataAsync();
        }
    }
}
