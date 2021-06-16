using Entities;
using Prism.Mvvm;

namespace EnrolleeGuide.ViewModels
{
    public class TrainingFormTypeRecord : BindableBase
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        private TrainingFormType _value;

        public TrainingFormType Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
    }
}
