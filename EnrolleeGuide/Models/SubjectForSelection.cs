using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolleeGuide.Models
{
    class SubjectForSelection : SubjectModel
    {
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
    }
}
