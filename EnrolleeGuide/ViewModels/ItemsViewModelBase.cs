using EnrolleeGuide.Stores;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace EnrolleeGuide.ViewModels
{
    public abstract class ItemsViewModelBase<TItem> : BindableBase, INavigationAware where TItem : new()
    {
        protected readonly IStore<TItem> _store;


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
                if (SetProperty(ref _selectedItem, value))
                {
                    RaisePropertyChanged(nameof(IsItemSelected));
                }
            }
        }

        public bool IsItemSelected => SelectedItem != null;

        public DelegateCommand<TItem> SelectCommand { get; private set; }

        public DelegateCommand CreateCommand { get; private set; }

        public DelegateCommand<TItem> SaveCommand { get; private set; }

        public DelegateCommand<TItem> DeleteCommand { get; private set; }

        public ItemsViewModelBase(string title, IStore<TItem> store)
        {
            Title = title;
            _store = store;

            SelectCommand = new DelegateCommand<TItem>(async item => await LoadItemAsync(item));
            CreateCommand = new DelegateCommand(Create);
            SaveCommand = new DelegateCommand<TItem>(async item => await SaveAsync(item));
            DeleteCommand = new DelegateCommand<TItem>(async item => await DeleteAsync(item));
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await OnNavigatedToImpl(navigationContext);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        protected abstract string DeleteConfirmationMessage(TItem item);

        protected virtual async Task OnNavigatedToImpl(NavigationContext navigationContext)
        {
            SelectedItem = default;

            await LoadItemsAsync();
        }

        protected virtual async Task LoadItemsAsync()
        {
            var items = await _store.GetAllAsync();

            Items = new ObservableCollection<TItem>(items);
        }

        protected virtual void Create()
        {
            SelectedItem = new TItem();
        }

        private async Task LoadItemAsync(TItem itemModel)
        {
            var loadedItem = await _store.GetAsync(itemModel);

            SelectedItem = loadedItem;
        }

        private async Task SaveAsync(TItem item)
        {
            await _store.SaveAsync(item);

            SelectedItem = default;

            await LoadItemsAsync();
        }

        private async Task DeleteAsync(TItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var messageBoxResult = MessageBox.Show(DeleteConfirmationMessage(item), "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult != MessageBoxResult.Yes)
            {
                return;
            }

            await _store.DeleteAsync(item);

            SelectedItem = default;

            await LoadItemsAsync();
        }
    }
}
