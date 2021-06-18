using Entities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolleeGuide.Models
{
    public class UniversityCriteria : BindableBase
    {
        private string _query;
        public string Query
        {
            get => _query;
            set => SetProperty(ref _query, value);
        }

        private int? _cityId;
        public int? CityId
        {
            get => _cityId;
            set => SetProperty(ref _cityId, value);
        }

        private int? _specialityId;
        public int? SpecialityId
        {
            get => _specialityId;
            set => SetProperty(ref _specialityId, value);
        }

        public ICollection<int> SubjectIds { get; set; }

        public ICollection<TrainingFormType> TrainingFormTypes { get; set; }
    }
}
