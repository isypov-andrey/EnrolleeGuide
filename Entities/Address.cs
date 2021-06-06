using System.ComponentModel.DataAnnotations;

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
    }
}
