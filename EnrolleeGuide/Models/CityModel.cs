﻿using Entities;
using Prism.Mvvm;

namespace EnrolleeGuide.Models
{
    public class CityModel : BindableBase
    {
        public int Id { get; set; }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public static CityModel GetFromDomain(City city)
        {
            if (city == null)
            {
                return null;
            }

            return new CityModel
            {
                Id = city.Id,
                Name = city.Name
            };
        }

        public City ToDomain()
        {
            return new City
            {
                Id = Id,
                Name = Name
            };
        }
    }
}
