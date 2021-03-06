using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Subject
    {

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
    }
}
