using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolleeGuide.Models
{
    class ToggleListItem<T> : BindableBase
    {
        private T _value;

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetProperty(ref _value, value);
            }
        }

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

        private bool _checked;

        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                SetProperty(ref _checked, value);
            }
        }

        public ToggleListItem(T value, string name)
        {
            this.Value = value;
            this.Name = name;
        }
    }
}
