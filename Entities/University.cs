using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class University
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
        public int CityId { get; set; }

        /// <summary>
        /// Город расположения
        /// </summary>
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        /// <summary>
        /// Адреса
        /// </summary>
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
