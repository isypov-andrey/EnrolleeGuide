using Prism.Mvvm;
using System.ComponentModel;
using System.Linq;

namespace EnrolleeGuide.Models
{
    public abstract class ValidatableModel : BindableBase, IDataErrorInfo
    {
        private bool _hasErrors;

        public bool HasErrors
        {
            get => _hasErrors;
            set => SetProperty(ref _hasErrors, value);
        }

        public virtual string Error => null;

        public string this[string columnName]
        {
            get
            {
                var error = Validate(columnName);

                Revalidate();

                return error;
            }
        }

        protected abstract string Validate(string columnName);

        protected void Revalidate()
        {
            var isValid = true;

            foreach (var propertyName in GetType().GetProperties().Select(property => property.Name))
            {
                isValid &= string.IsNullOrEmpty(Validate(propertyName));
            }

            HasErrors = !isValid;
        }
    }
}
