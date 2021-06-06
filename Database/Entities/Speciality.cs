using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class Speciality
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
