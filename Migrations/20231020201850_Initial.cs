using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShoppingWEB.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "__EFMigrationsHistory",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    ProductVersion = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MigrationId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    customer_id = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    category_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    image_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    coupon_description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    discount_value = table.Column<int>(type: "int", nullable: true),
                    limited = table.Column<int>(type: "int", nullable: true),
                    start_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Delivery_INFO",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    receiver = table.Column<string>(type: "varchar(255)", nullable: true),
                    street = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    ward = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    district = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    city = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    @default = table.Column<bool>(name: "default", type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeliveryProvider",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    deliveryprovider_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    price = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentINFO",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    payment_method = table.Column<string>(type: "enum('VISA','MASTERCARD','BANKING')", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    product_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    price = table.Column<double>(type: "double", nullable: true),
                    discount_percent = table.Column<double>(type: "double", nullable: true),
                    in_stock = table.Column<int>(type: "int", nullable: true),
                    short_description = table.Column<string>(type: "text", nullable: true),
                    product_description = table.Column<string>(type: "longtext", nullable: true),
                    published = table.Column<sbyte>(type: "tinyint", nullable: true),
                    category_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "Products_ibfk_1",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    customer_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    payment_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    coupon_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "Order_ibfk_1",
                        column: x => x.payment_id,
                        principalTable: "PaymentINFO",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "Order_ibfk_2",
                        column: x => x.coupon_id,
                        principalTable: "Coupons",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    product_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    cart_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    quanity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "CartItems_ibfk_1",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "CartItems_ibfk_2",
                        column: x => x.cart_id,
                        principalTable: "Cart",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ImageURL",
                columns: table => new
                {
                    image_path = table.Column<string>(type: "varchar(255)", nullable: false),
                    product_id = table.Column<string>(type: "varchar(255)", nullable: false),
                    thumnail = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.image_path, x.product_id });
                    table.ForeignKey(
                        name: "ImageURL_Products_id_fk",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductCoupons",
                columns: table => new
                {
                    coupon_id = table.Column<string>(type: "varchar(255)", nullable: false),
                    product_id = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.coupon_id, x.product_id });
                    table.ForeignKey(
                        name: "ProductCoupons_ibfk_1",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ProductCoupons_ibfk_2",
                        column: x => x.coupon_id,
                        principalTable: "Coupons",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    order_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    delivery_provider_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    quanity = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "OrderItem_ibfk_1",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "OrderItem_ibfk_2",
                        column: x => x.delivery_provider_id,
                        principalTable: "DeliveryProvider",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "customer_id",
                table: "Cart",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "cart_id",
                table: "CartItems",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "product_id1",
                table: "CartItems",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "receiver",
                table: "Delivery_INFO",
                column: "receiver");

            migrationBuilder.CreateIndex(
                name: "ImageURL_Products_id_fk",
                table: "ImageURL",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "coupon_id",
                table: "Order",
                column: "coupon_id");

            migrationBuilder.CreateIndex(
                name: "customer_id1",
                table: "Order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "payment_id",
                table: "Order",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "delivery_provider_id",
                table: "OrderItem",
                column: "delivery_provider_id");

            migrationBuilder.CreateIndex(
                name: "order_id",
                table: "OrderItem",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "product_id",
                table: "ProductCoupons",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "category_id",
                table: "Products",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__EFMigrationsHistory");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Delivery_INFO");

            migrationBuilder.DropTable(
                name: "ImageURL");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductCoupons");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "DeliveryProvider");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PaymentINFO");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    AvtPath = table.Column<string>(type: "longtext", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FirstName = table.Column<string>(type: "longtext", nullable: true),
                    Gender = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LastName = table.Column<string>(type: "longtext", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }
    }
}
