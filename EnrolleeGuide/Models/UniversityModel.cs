using Entities;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace EnrolleeGuide.Models
{
    public class UniversityModel : BindableBase
    {
        public int Id { get; set; }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// <see cref="Description"/>
        /// </summary>
        private string _description;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        /// <summary>
        /// <see cref="CityId"/>
        /// </summary>
        private int _cityId;

        /// <summary>
        /// Идентификатор города расположения
        /// </summary>
        public int CityId
        {
            get => _cityId;
            set => SetProperty(ref _cityId, value);
        }

        /// <summary>
        /// <see cref="Addresses"/>
        /// </summary>
        private ObservableCollection<AddressModel> _addresses = new ObservableCollection<AddressModel>();

        /// <summary>
        /// Адреса
        /// </summary>
        public ObservableCollection<AddressModel> Addresses
        {
            get => _addresses;
            set => SetProperty(ref _addresses, value);
        }

        public static UniversityModel GetFromDomain(University university)
        {
            if (university == null)
            {
                return null;
            }

            var universityModel = new UniversityModel
            {
                Id = university.Id,
                Name = university.Name,
                Description = university.Description,
                CityId = university.CityId
            };
            if (university.Addresses?.Any() == true)
            {
                universityModel.Addresses.AddRange(
                    university.Addresses.Select(AddressModel.GetFromDomain)
                );
            }

            return universityModel;
        }

        public University ToDomain()
        {
            return new University
            {
                Id = Id,
                Name = Name,
                Description = Description,
                CityId = CityId,
                Addresses = Addresses.Select(address => address.ToDomain())
                    .ToList()
            };
        }
    }
}
