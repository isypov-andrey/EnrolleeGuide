using Entities;
using Prism.Mvvm;

namespace EnrolleeGuide.Models
{
    public class SubjectModel : BindableBase
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

        public static SubjectModel GetFromDomain(Subject subject)
        {
            if (subject == null)
            {
                return null;
            }

            return new SubjectModel
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }

        public Subject ToDomain()
        {
            return new Subject
            {
                Id = Id,
                Name = Name
            };
        }
    }
}
