using System;
using System.Collections.Generic;
using Kitchen_Appliances_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen_Appliances_Backend.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUserToken> AppUserTokens { get; set; }
    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> Orderdetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPrice> ProductPrices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-E2I98S5\\SQLEXPRESS;Initial Catalog=BAN_DUNG_CU_NHA_BEP;User ID=sa;Password=1234;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email);

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_ACCOUNT_ROLE");

        });

        modelBuilder.Entity<AppUserToken>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity
                .ToTable("AppUserToken");

            entity.Property(e => e.AccountId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Token).HasMaxLength(50);
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.AppUserTokens)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_AppUserToken_ACCOUNT");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("BILL");

            entity.Property(e => e.OrderId).ValueGeneratedOnAdd().UseIdentityColumn();
            entity.Property(e => e.PaymentTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.Order).WithOne(p => p.Bill)
                .HasForeignKey<Bill>(d => d.OrderId)
                .HasConstraintName("FK_BILL_ORDER");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.ProductId });

            entity.ToTable("CartDetail");

            entity.HasOne(d => d.Customer).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CartDetail_CUSTOMER");

            entity.HasOne(d => d.Product).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_CartDetail_PRODUCT");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("CATEGORY");

            entity.HasIndex(e => e.Id, "UK_CATEGORY").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("CUSTOMER");

            entity.HasIndex(e => e.Id, "UK_CUSTOMER").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn(); ;
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fullname).HasMaxLength(40);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK_CUSTOMER_ACCOUNT");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("EMPLOYEE");

            entity.HasIndex(e => e.Id, "UK_EMPLOYEE").IsUnique();

            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd().UseIdentityColumn();
  
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fullname).HasMaxLength(40);
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK_EMPLOYEE_ACCOUNT");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("IMAGE");

            entity.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_IMAGE_PRODUCT");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("ORDER");

            entity.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_ORDER_CUSTOMER");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_ORDER_EMPLOYEE");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("ORDERDETAIL");

            entity.HasIndex(e => new { e.OrderId, e.ProductId }, "UK_ORDERDETAIL").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_ORDERDETAIL_ORDER");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ORDERDETAIL_PRODUCT");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("PRODUCT");

            entity.HasIndex(e => e.Id, "UK_PRODUCT").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_PRODUCT_CATEGORY");
        });

        modelBuilder.Entity<ProductPrice>(entity =>
        {
            entity.ToTable("PRODUCTPRICE");

            entity.HasIndex(e => new { e.AppliedDate, e.ProductId }, "UK_PRODUCTPRICE").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AppliedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Price).HasColumnType("money");

            //entity.HasOne(d => d.Employee).WithMany(p => p.Productprices)
            //    .HasForeignKey(d => d.EmployeeId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_PRODUCTPRICE_EMPLOYEE");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPrices)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_PRODUCTPRICE_PRODUCT");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("ROLE");

            entity.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn(); ;
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
// command generate scaffold        from database
//dotnet ef dbcontext scaffold "Data Source=DESKTOP-E2I98S5\\SQLEXPRESS;Initial Catalog=BAN_DUNG_CU_NHA_BEP;User ID=sa;Password=1234;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Models --context BanDungCuNhaBepContext --context-dir Data
// link: https://learn.microsoft.com/en-us/ef/core/cli/dotnet