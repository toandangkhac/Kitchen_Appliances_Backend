using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kitchen_Appliances_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUserToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRODUCT_CATEGORY",
                        column: x => x.CategoryId,
                        principalTable: "CATEGORY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNT",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNT", x => x.Email);
                    table.ForeignKey(
                        name: "FK_ACCOUNT_ROLE",
                        column: x => x.RoleId,
                        principalTable: "ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IMAGE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMAGE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IMAGE_PRODUCT",
                        column: x => x.ProductId,
                        principalTable: "PRODUCT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    AccountId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserToken_ACCOUNT",
                        column: x => x.AccountId,
                        principalTable: "ACCOUNT",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_ACCOUNT",
                        column: x => x.Email,
                        principalTable: "ACCOUNT",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EMPLOYEE_ACCOUNT",
                        column: x => x.Email,
                        principalTable: "ACCOUNT",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartDetail",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetail", x => new { x.CustomerId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CartDetail_CUSTOMER",
                        column: x => x.CustomerId,
                        principalTable: "CUSTOMER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDetail_PRODUCT",
                        column: x => x.ProductId,
                        principalTable: "PRODUCT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORDER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ORDER_CUSTOMER",
                        column: x => x.CustomerId,
                        principalTable: "CUSTOMER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ORDER_EMPLOYEE",
                        column: x => x.EmployeeID,
                        principalTable: "EMPLOYEE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTPRICE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTPRICE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRODUCTPRICE_EMPLOYEE",
                        column: x => x.EmployeeID,
                        principalTable: "EMPLOYEE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PRODUCTPRICE_PRODUCT",
                        column: x => x.ProductId,
                        principalTable: "PRODUCT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BILL",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PaymentTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Total = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BILL", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_BILL_ORDER",
                        column: x => x.OrderId,
                        principalTable: "ORDER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORDERDETAIL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERDETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ORDERDETAIL_ORDER",
                        column: x => x.OrderId,
                        principalTable: "ORDER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ORDERDETAIL_PRODUCT",
                        column: x => x.ProductId,
                        principalTable: "PRODUCT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNT_RoleId",
                table: "ACCOUNT",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserToken_AccountId",
                table: "AppUserToken",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_ProductId",
                table: "CartDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "UK_CATEGORY",
                table: "CATEGORY",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_Email",
                table: "CUSTOMER",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "UK_CUSTOMER",
                table: "CUSTOMER",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_Email",
                table: "EMPLOYEE",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "UK_EMPLOYEE",
                table: "EMPLOYEE",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IMAGE_ProductId",
                table: "IMAGE",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_CustomerId",
                table: "ORDER",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_EmployeeID",
                table: "ORDER",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERDETAIL_ProductId",
                table: "ORDERDETAIL",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "UK_ORDERDETAIL",
                table: "ORDERDETAIL",
                columns: new[] { "OrderId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_CategoryId",
                table: "PRODUCT",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "UK_PRODUCT",
                table: "PRODUCT",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTPRICE_EmployeeID",
                table: "PRODUCTPRICE",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTPRICE_ProductId",
                table: "PRODUCTPRICE",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "UK_PRODUCTPRICE",
                table: "PRODUCTPRICE",
                columns: new[] { "AppliedDate", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserToken");

            migrationBuilder.DropTable(
                name: "BILL");

            migrationBuilder.DropTable(
                name: "CartDetail");

            migrationBuilder.DropTable(
                name: "IMAGE");

            migrationBuilder.DropTable(
                name: "ORDERDETAIL");

            migrationBuilder.DropTable(
                name: "PRODUCTPRICE");

            migrationBuilder.DropTable(
                name: "ORDER");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "CUSTOMER");

            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "CATEGORY");

            migrationBuilder.DropTable(
                name: "ACCOUNT");

            migrationBuilder.DropTable(
                name: "ROLE");
        }
    }
}
