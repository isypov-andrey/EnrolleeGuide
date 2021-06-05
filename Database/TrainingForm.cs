using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class TrainingForm
    {
        public int Id { get; set; }
        /// <summary>
        /// Тип формы обучения
        /// </summary>
        public TrainingFormType Type { get; set; }
        /// <summary>
        /// Стоимость обучения на платном
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// Продолжительность обучения лет
        /// </summary>
        public int DurationInYears { get; set; }
        /// <summary>
        /// Минимальный проходной балл на бюджет
        /// </summary>
        public int? BudgetExamPoints { get; set; }
        /// <summary>
        /// Количество бюджетных мест
        /// </summary>
        public int? BudgetPlacesCount { get; set; }
    }
}
