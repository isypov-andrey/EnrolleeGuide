using EnrolleeGuide.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using System.Windows.Threading;

namespace EnrolleeGuide
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CityItemsView>();
            containerRegistry.RegisterForNavigation<SpecialityItemsView>();
            containerRegistry.RegisterForNavigation<SubjectItemsView>();
            containerRegistry.RegisterForNavigation<UniversityItemsView>();
            containerRegistry.RegisterForNavigation<UniversitiesMainView>();
            containerRegistry.RegisterForNavigation<ProgramItemsView>();
        }

        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = string.Format("Произошла ошибка: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
