// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RevenueAITask.Models
{
    [Table("currency")]
    public partial class Currency
    {
        public Currency()
        {
            Cards = new HashSet<Card>();
        }

        [Key]
        [Column("CurrencyID")]
        public int CurrencyId { get; set; }
        [Required]
        [Column("Currency")]
        [StringLength(50)]
        public string Currency1 { get; set; }

        [InverseProperty(nameof(Card.Currency))]
        public virtual ICollection<Card> Cards { get; set; }
    }
}