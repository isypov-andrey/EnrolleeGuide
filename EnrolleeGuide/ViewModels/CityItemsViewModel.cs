using EnrolleeGuide.Models;
using EnrolleeGuide.Stores;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolleeGuide.ViewModels
{
    public class CityItemsViewModel : ItemsViewModelBase<CityModel>
    {
        public CityItemsViewModel(CitiesStore store) : base("Города", store)
        {

        }
    }
}
