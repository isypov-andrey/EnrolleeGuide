using Entities;
using Prism.Mvvm;

namespace EnrolleeGuide.Models
{
    public class TrainingFormModel : BindableBase
    {
        public int Id { get; set; }

        /// <summary>
        /// <see cref="Type"/>
        /// </summary>
        private TrainingFormType _type;

        /// <summary>
        /// Тип формы обучения
        /// </summary>
        public TrainingFormType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        /// <summary>
        /// <see cref="Price"/>
        /// </summary>
        private decimal? _price;

        /// <summary>
        /// Стоимость обучения на платном
        /// </summary>
        public decimal? Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        /// <summary>
        /// <see cref="DurationInYears"/>
        /// </summary>
        private int _durationInYears;

        /// <summary>
        /// Продолжительность обучения лет
        /// </summary>
        public int DurationInYears
        {
            get => _durationInYears;
            set => SetProperty(ref _durationInYears, value);
        }

        /// <summary>
        /// <see cref="BudgetExamPoints"/>
        /// </summary>
        private int? _budgetExamPoints;

        /// <summary>
        /// Минимальный проходной балл на бюджет
        /// </summary>
        public int? BudgetExamPoints
        {
            get => _budgetExamPoints;
            set => SetProperty(ref _budgetExamPoints, value);
        }

        /// <summary>
        /// <see cref="BudgetPlacesCount"/>
        /// </summary>
        private int? _budgetPlacesCount;

        /// <summary>
        /// Количество бюджетных мест
        /// </summary>
        public int? BudgetPlacesCount
        {
            get => _budgetPlacesCount;
            set => SetProperty(ref _budgetPlacesCount, value);
        }

        public static TrainingFormModel GetFromDomain(TrainingForm trainingForm)
        {
            if (trainingForm == null)
            {
                return null;
            }

            return new TrainingFormModel
            {
                Id = trainingForm.Id,
                Type = trainingForm.Type,
                Price = trainingForm.Price,
                DurationInYears = trainingForm.DurationInYears,
                BudgetExamPoints = trainingForm.BudgetExamPoints,
                BudgetPlacesCount = trainingForm.BudgetPlacesCount
            };
        }

        public TrainingForm ToDomain()
        {
            return new TrainingForm
            {
                Id = Id,
                Type = Type,
                Price = Price,
                DurationInYears = DurationInYears,
                BudgetExamPoints = BudgetExamPoints,
                BudgetPlacesCount = BudgetPlacesCount
            };
        }
    }
}
