using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class University
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
        /// Город расположения
        /// </summary>
        public City City { get; set; }
        /// <summary>
        /// Адреса
        /// </summary>
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
