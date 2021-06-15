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
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
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
            get
            {
                return _description;
            }
            set
            {
                SetProperty(ref _description, value);
            }
        }

        /// <summary>
        /// Адреса
        /// </summary>
        public ObservableCollection<AddressModel> Addresses { get; set; } = new ObservableCollection<AddressModel>();

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
                Description = university.Description
            };
            if (university.Addresses?.Any() == true)
            {
                universityModel.Addresses.AddRange(
                    university.Addresses.Select(
                        address => new AddressModel
                        {
                            Id = address.Id,
                            FullAddress = address.FullAddress,
                            Phone = address.Phone
                        }
                    )
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
                Addresses = Addresses.Select(
                        address => new Address
                        {
                            Id = address.Id,
                            FullAddress = address.FullAddress,
                            Phone = address.Phone
                        }
                    )
                    .ToList()
            };
        }
    }
}
