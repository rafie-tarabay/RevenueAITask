﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RevenueAITask.Models
{
    [Table("Account")]
    public partial class Account
    {
        [Key]
        [Column("AccountID")]
        public int AccountId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        public double? Balance { get; set; }
        [Column("AccountTypeID")]
        public int AccountTypeId { get; set; }

        [ForeignKey(nameof(AccountTypeId))]
        [InverseProperty("Accounts")]
        public virtual AccountType AccountType { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Accounts")]
        public virtual User User { get; set; }
    }
}