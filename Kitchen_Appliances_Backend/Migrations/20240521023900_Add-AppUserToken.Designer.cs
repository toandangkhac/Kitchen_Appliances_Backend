﻿// <auto-generated />
using System;
using Kitchen_Appliances_Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kitchen_Appliances_Backend.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240521023900_Add-AppUserToken")]
    partial class AddAppUserToken
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Account", b =>
                {
                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Email");

                    b.HasIndex("RoleId");

                    b.ToTable("ACCOUNT", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.AppUserToken", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("AccountId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("ExpiredAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AppUserToken", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Bill", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<decimal>("Total")
                        .HasColumnType("money");

                    b.HasKey("OrderId");

                    b.ToTable("BILL", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.CartDetail", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CustomerId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartDetail", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "UK_CATEGORY")
                        .IsUnique();

                    b.ToTable("CATEGORY", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex(new[] { "Id" }, "UK_CUSTOMER")
                        .IsUnique();

                    b.ToTable("CUSTOMER", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Image")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex(new[] { "Id" }, "UK_EMPLOYEE")
                        .IsUnique();

                    b.ToTable("EMPLOYEE", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("IMAGE", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ORDER", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex(new[] { "OrderId", "ProductId" }, "UK_ORDERDETAIL")
                        .IsUnique();

                    b.ToTable("ORDERDETAIL", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<bool?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex(new[] { "Id" }, "UK_PRODUCT")
                        .IsUnique();

                    b.ToTable("PRODUCT", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Productprice", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("AppliedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProductId");

                    b.HasIndex(new[] { "AppliedDate", "ProductId" }, "UK_PRODUCTPRICE")
                        .IsUnique();

                    b.ToTable("PRODUCTPRICE", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("ROLE", (string)null);
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Account", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ACCOUNT_ROLE");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.AppUserToken", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Account", "Account")
                        .WithMany("AppUserTokens")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_AppUserToken_ACCOUNT");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Bill", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Order", "Order")
                        .WithOne("Bill")
                        .HasForeignKey("Kitchen_Appliances_Backend.Models.Bill", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BILL_ORDER");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.CartDetail", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Customer", "Customer")
                        .WithMany("CartDetails")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CartDetail_CUSTOMER");

                    b.HasOne("Kitchen_Appliances_Backend.Models.Product", "Product")
                        .WithMany("CartDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CartDetail_PRODUCT");

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Customer", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Account", "EmailNavigation")
                        .WithMany("Customers")
                        .HasForeignKey("Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CUSTOMER_ACCOUNT");

                    b.Navigation("EmailNavigation");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Employee", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Account", "EmailNavigation")
                        .WithMany("Employees")
                        .HasForeignKey("Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_EMPLOYEE_ACCOUNT");

                    b.Navigation("EmailNavigation");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Image", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_IMAGE_PRODUCT");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Order", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ORDER_CUSTOMER");

                    b.HasOne("Kitchen_Appliances_Backend.Models.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK_ORDER_EMPLOYEE");

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.OrderDetail", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ORDERDETAIL_ORDER");

                    b.HasOne("Kitchen_Appliances_Backend.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ORDERDETAIL_PRODUCT");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Product", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PRODUCT_CATEGORY");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Productprice", b =>
                {
                    b.HasOne("Kitchen_Appliances_Backend.Models.Employee", "Employee")
                        .WithMany("Productprices")
                        .HasForeignKey("EmployeeId")
                        .IsRequired()
                        .HasConstraintName("FK_PRODUCTPRICE_EMPLOYEE");

                    b.HasOne("Kitchen_Appliances_Backend.Models.Product", "Product")
                        .WithMany("Productprices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PRODUCTPRICE_PRODUCT");

                    b.Navigation("Employee");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Account", b =>
                {
                    b.Navigation("AppUserTokens");

                    b.Navigation("Customers");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Customer", b =>
                {
                    b.Navigation("CartDetails");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Employee", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Productprices");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Order", b =>
                {
                    b.Navigation("Bill");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Product", b =>
                {
                    b.Navigation("CartDetails");

                    b.Navigation("Images");

                    b.Navigation("OrderDetails");

                    b.Navigation("Productprices");
                });

            modelBuilder.Entity("Kitchen_Appliances_Backend.Models.Role", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
