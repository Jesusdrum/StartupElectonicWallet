using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartupElectonicWallet.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Required]
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        [Required]
        [ForeignKey("TransactionType")]
        public String TransactionType { get; set; }

        [ForeignKey("Account")]
        public int AccountToId { get; set; }
        [Required]
        public Decimal Amount { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
