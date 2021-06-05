using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Address
    {
        public int Id { get; set; }
        /// <summary>
        /// Полный адрес
        /// </summary>
        public string FullAddress { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }
    }
}
