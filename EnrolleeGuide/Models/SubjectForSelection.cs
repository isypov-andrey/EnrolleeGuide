namespace EnrolleeGuide.Models
{
    public class SubjectForSelection : SubjectModel
    {
        private bool _checked;

        public bool Checked
        {
            get => _checked;
            set => SetProperty(ref _checked, value);
        }
    }
}
