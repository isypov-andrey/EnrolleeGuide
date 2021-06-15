using EnrolleeGuide.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace EnrolleeGuide
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CityItemsView>();
            containerRegistry.RegisterForNavigation<SpecialityItemsView>();
            containerRegistry.RegisterForNavigation<SubjectItemsView>();
            containerRegistry.RegisterForNavigation<UniversityItemsView>();
        }

        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }
    }
}
