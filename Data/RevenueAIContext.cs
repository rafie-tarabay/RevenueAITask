// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RevenueAITask.Models;

#nullable disable

namespace RevenueAITask.Data
{
    public partial class RevenueAIContext : DbContext
    {
        public RevenueAIContext()
        {
        }

        public RevenueAIContext(DbContextOptions<RevenueAIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<CardState> CardStates { get; set; }
        public virtual DbSet<CardType> CardTypes { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<PasswordRecoveryHistory> PasswordRecoveryHistories { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountType>()
    .HasData(new List<AccountType>
    {
                      new AccountType { AccountTypeId = 1, AccountTypeDesc = "Deposit"},
                      new AccountType { AccountTypeId = 2, AccountTypeDesc = "Credit"},
                      new AccountType { AccountTypeId = 3, AccountTypeDesc = "Currency"},
    });

            modelBuilder.Entity<CardState>()
                .HasData(new List<CardState>
                {
                    new CardState { StateId = 1, StateDescription = "Active"},
                    new CardState { StateId = 2, StateDescription = "Inactive"},
                    new CardState { StateId = 3, StateDescription = "Disabled"},
                    new CardState { StateId = 4, StateDescription = "Expired"},
                });



            modelBuilder.Entity<CardType>()
                .HasData(new List<CardType>
                {
                    new CardType { CardTypeId = 1, CardType1 = "Forint"},
                    new CardType { CardTypeId = 2, CardType1 = "Currency"},
                    new CardType { CardTypeId = 3, CardType1 = "Credit"},
                });


            modelBuilder.Entity<Currency>()
                .HasData(new List<Currency>
                {
                    new Currency { CurrencyId = 1, Currency1 = "EUR"},
                    new Currency { CurrencyId = 2, Currency1 = "USD"},
                });

            modelBuilder.Entity<TransactionType>()
                .HasData(new List<TransactionType>
                {
                    new TransactionType { TransactionTypeId = 1, TransactionTypeName = "Normal"},
                    new TransactionType { TransactionTypeId = 2, TransactionTypeName = "Cancelled"},
                });

            modelBuilder.Entity<UserType>()
                .HasData(new List<UserType>
                {
                    new UserType { UserTypeId = 1, UserTypeDesc = "Admin"},
                    new UserType { UserTypeId = 2, UserTypeDesc = "User"},
                });


            modelBuilder.Entity<Vendor>()
                .HasData(new List<Vendor>
                {
                    new Vendor { VendorId = 1, Name = "RevenueAI"},
                });

            modelBuilder.Entity<User>()
                .HasData(new List<User>
                {
                    new User { UserId = 1, FirstName = "Mohamed" ,LastName="Rafie",UserName="eng.rafie@gmail.com",Password="TY+50vQHnqNKj7SMs9ipJotI8TDePS0HZ2DyUmoovYk=",UserTypeId=1},
                });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_AccountType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_User");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.Property(e => e.AccountTypeDesc).IsUnicode(false);
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.CardNumber)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Valid)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CardType)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CardTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Card_CardType");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Card_currency");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Card_CardState");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Card_User");
            });

            modelBuilder.Entity<CardState>(entity =>
            {
                entity.Property(e => e.StateDescription).IsUnicode(false);
            });

            modelBuilder.Entity<CardType>(entity =>
            {
                entity.Property(e => e.CardType1).IsUnicode(false);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Currency1).IsUnicode(false);
            });

            modelBuilder.Entity<PasswordRecoveryHistory>(entity =>
            {
                entity.Property(e => e.IsUsed).IsUnicode(false);

                entity.Property(e => e.RecoveryInitialPassword).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PasswordRecoveryHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PasswordRecoveryHistory_User");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.CardNumber)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CardNumberNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CardNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Card");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_TransactionTypes");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Vendor");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.Property(e => e.TransactionTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.UserTypeDesc).IsFixedLength(true);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Mail).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}