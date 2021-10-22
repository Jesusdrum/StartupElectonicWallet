using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartupElectonicWallet.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [ForeignKey("Customer")]
        [Required]
        public int CustomerId { get; set; }
        [Required]

        public int StatusId { get; set; }
        public Decimal AvailableAmount { get; set; }
        public DateTime DateCreate { get; set; }

        [ForeignKey("Transaction")]
        public List<Transaction> TransactionCollection { get; set; }

    }
}
