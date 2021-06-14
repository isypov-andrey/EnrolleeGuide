using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Database;
using Database.Repositories;
using EnrolleeGuide.Stores;
using EnrolleeGuide.Views;
using Prism.Ioc;
using Prism.Unity;

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

            containerRegistry.Register<CitiesStore>();

            containerRegistry.Register<CityRepository>();
            containerRegistry.Register<DataContext>();
        }

        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }
    }
}
