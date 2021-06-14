using Entities;
using Prism.Mvvm;

namespace EnrolleeGuide.Models
{
    public class SpecialityModel : BindableBase
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

        public static SpecialityModel GetFromDomain(Speciality speciality)
        {
            if (speciality == null)
            {
                return null;
            }

            return new SpecialityModel
            {
                Id = speciality.Id,
                Name = speciality.Name
            };
        }

        public Speciality ToDomain()
        {
            return new Speciality
            {
                Id = Id,
                Name = Name
            };
        }
    }
}
