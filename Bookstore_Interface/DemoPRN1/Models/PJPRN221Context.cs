﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoPRN1.Models
{
    public partial class PJPRN221Context : DbContext
    {
        public PJPRN221Context()
        {
        }

        public PJPRN221Context(DbContextOptions<PJPRN221Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Bookrating> Bookratings { get; set; } = null!;
        public virtual DbSet<Bookstore> Bookstores { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Oder> Oders { get; set; } = null!;
        public virtual DbSet<Oderdetail> Oderdetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("Account_ID");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(30);

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumer)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Account__Role_ID__267ABA7A");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId).HasColumnName("Book_Id");

                entity.Property(e => e.Author).HasMaxLength(30);

                entity.Property(e => e.BookStoreId).HasColumnName("BookStore_ID");

                entity.Property(e => e.BookTitle).HasColumnName("Book_title");

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RentalPrice).HasColumnType("money");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.BookStore)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BookStoreId)
                    .HasConstraintName("FK__Books__BookStore__2E1BDC42");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Books__Category___2F10007B");
            });

            modelBuilder.Entity<Bookrating>(entity =>
            {
                entity.HasKey(e => e.RatingId)
                    .HasName("PK__Bookrati__BE48C8457D67B2B0");

                entity.ToTable("Bookrating");

                entity.Property(e => e.RatingId).HasColumnName("Rating_Id");

                entity.Property(e => e.BookId).HasColumnName("Book_Id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Bookratings)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Bookratin__Book___31EC6D26");
            });

            modelBuilder.Entity<Bookstore>(entity =>
            {
                entity.Property(e => e.BookStoreId).HasColumnName("BookStore_ID");

                entity.Property(e => e.AccountId).HasColumnName("Account_ID");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Bookstores)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Bookstore__Accou__29572725");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.CategoryName).HasMaxLength(30);
            });

            modelBuilder.Entity<Oder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Oders__F1E4639BC63BBBC1");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.AccountId).HasColumnName("Account_ID");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Oders)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Oders__Account_I__34C8D9D1");
            });

            modelBuilder.Entity<Oderdetail>(entity =>
            {
                entity.HasKey(e => e.OrderdetailsId)
                    .HasName("PK__Oderdeta__CC54DA73D90AE48D");

                entity.Property(e => e.OrderdetailsId).HasColumnName("Orderdetails_ID");

                entity.Property(e => e.BookId).HasColumnName("Book_Id");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Startdate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Oderdetails)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Oderdetai__Book___37A5467C");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
