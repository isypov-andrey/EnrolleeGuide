using Entities;

namespace EnrolleeGuide.Models
{
    public class CityModel : ValidatableModel
    {
        public int Id { get; set; }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
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

        protected override string Validate(string columnName)
        {
            var error = string.Empty;
            switch (columnName)
            {
                case nameof(Name):
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        error = "Обязательное поле";
                    }
                    break;
                default:
                    break;
            }

            HasErrors = error != string.Empty;

            return error;
        }
    }
}
