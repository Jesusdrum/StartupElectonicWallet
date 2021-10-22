using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartupElectonicWallet.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Required]
        public string CustomerIdentifier { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Addreess { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        public DateTime DateCreate { get; set; }
        [Required]
        public int StatusId { get; set; }
        public List<Models.Account> AccountCollection { get; set; }

    }
}
