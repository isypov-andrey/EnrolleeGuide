using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Program
    {
        public int Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Университет программы
        /// </summary>
        public virtual University University { get; set; }
        /// <summary>
        /// Специальность
        /// </summary>
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
