using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ListaZakupow.Models;

public partial class ShoppingListContext : DbContext
{
    public ShoppingListContext()
    {
    }

    public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }

    public virtual DbSet<ShoppingListItem> ShoppingListItems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["ListaZakupowDB"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduktu).HasName("PK__Produkty__A3EEA960F702C871");

            entity.Property(e => e.IdProduktu).HasColumnName("ID_Produktu");
            entity.Property(e => e.Cena)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Kategoria).HasMaxLength(100);
            entity.Property(e => e.NazwaProduktu).HasMaxLength(255);
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(e => e.IdListy).HasName("PK__ListyZak__B5A70F2AA68AE0E8");

            entity.Property(e => e.IdListy).HasColumnName("ID_Listy");
            entity.Property(e => e.IdUzytkownika).HasColumnName("ID_Uzytkownika");
            entity.Property(e => e.NazwaListy).HasMaxLength(255);

            entity.HasOne(d => d.IdUzytkownikaNavigation).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.IdUzytkownika)
                .HasConstraintName("FK__ListyZaku__ID_Uz__3A81B327");
        });

        modelBuilder.Entity<ShoppingListItem>(entity =>
        {
            entity.HasKey(e => e.IdPozycji).HasName("PK__PozycjeL__676194992A9BF451");

            entity.Property(e => e.IdPozycji).HasColumnName("ID_Pozycji");
            entity.Property(e => e.IdListy).HasColumnName("ID_Listy");
            entity.Property(e => e.IdProduktu).HasColumnName("ID_Produktu");
            entity.Property(e => e.Ilosc).HasDefaultValue(1);

            entity.HasOne(d => d.IdListyNavigation).WithMany(p => p.ShoppingListItems)
                .HasForeignKey(d => d.IdListy)
                .HasConstraintName("FK__PozycjeLi__ID_Li__412EB0B6");

            entity.HasOne(d => d.IdProduktuNavigation).WithMany(p => p.ShoppingListItems)
                .HasForeignKey(d => d.IdProduktu)
                .HasConstraintName("FK__PozycjeLi__ID_Pr__4222D4EF");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownika).HasName("PK__Uzytkown__DAFA329C52379534");

            entity.HasIndex(e => e.Email, "UQ__Uzytkown__A9D10534DF2080C2").IsUnique();

            entity.Property(e => e.IdUzytkownika).HasColumnName("ID_Uzytkownika");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.NazwaUzytkownika).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
