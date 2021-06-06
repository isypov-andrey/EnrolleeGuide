using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Program
    {

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        [Required]
        public int UniversityId { get; set; }

        [Required]
        public int SpecialityId { get; set; }

        /// <summary>
        /// Университет программы
        /// </summary>
        [ForeignKey(nameof(UniversityId))]
        public virtual University University { get; set; }

        /// <summary>
        /// Специальность
        /// </summary>
        [ForeignKey(nameof(SpecialityId))]
        public virtual Speciality Speciality { get; set; }

        /// <summary>
        /// Формы обучения
        /// </summary>
        public virtual ICollection<TrainingForm> TrainingForms { get; set; }

        /// <summary>
        /// Предметы ЕГЭ
        /// </summary>
        public virtual ICollection<Subject> EgeSubjects { get; set; }
    }
}
