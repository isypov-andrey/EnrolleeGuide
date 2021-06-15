using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Полный адрес
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string FullAddress { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        public int UniversityId { get; set; }

        /// <summary>
        /// Университет
        /// </summary>
        [ForeignKey(nameof(UniversityId))]
        public virtual University University { get; set; }
    }
}
