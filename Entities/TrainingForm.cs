using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class TrainingForm
    {

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Тип формы обучения
        /// </summary>
        [Required]
        public TrainingFormType Type { get; set; }

        /// <summary>
        /// Стоимость обучения на платном
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Продолжительность обучения лет
        /// </summary>
        [Required]
        public int DurationInYears { get; set; }

        /// <summary>
        /// Минимальный проходной балл на бюджет
        /// </summary>
        public int? BudgetExamPoints { get; set; }

        /// <summary>
        /// Количество бюджетных мест
        /// </summary>
        public int? BudgetPlacesCount { get; set; }

        [Required]
        public int ProgramId { get; set; }

        [ForeignKey(nameof(ProgramId))]
        public Program Program { get; set; }
    }
}
