using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingWEB.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    AvtPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__3214EC0759460FA6", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImagePath = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__3214EC07BEECF95C", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CouponDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DiscountValue = table.Column<int>(type: "int", nullable: true),
                    Limited = table.Column<int>(type: "int", nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    EndAt = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Coupons__3214EC07105E2D15", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Delivery_INFO",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ReceiverId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Street = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Ward = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    District = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivery__3214EC07654CE19F", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivery__3214EC073B71C692", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PaymentName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__3214EC07A98C5F0B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seller",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SellerName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ReplyPercent = table.Column<int>(type: "int", nullable: true),
                    JoinAt = table.Column<DateTime>(type: "date", nullable: true),
                    Follower = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Seller__3214EC07DC4BD694", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CategoryId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    DiscountPercent = table.Column<double>(type: "float", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publish = table.Column<byte>(type: "tinyint", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Sold = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Style = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MadeIn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Material = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__3214EC0772F3CFDE", x => x.Id);
                    table.ForeignKey(
                        name: "Product_Seller_Id_fk",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Product_ibfk_1",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageURL",
                columns: table => new
                {
                    ImagePath = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Thumbnail = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ImageURL__67172E6694BF550B", x => x.ImagePath);
                    table.ForeignKey(
                        name: "ImageURL_Product_Id_fk",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCoupon",
                columns: table => new
                {
                    CouponId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductC__F30A3DD63B114D43", x => new { x.CouponId, x.ProductId });
                    table.ForeignKey(
                        name: "ProductCoupons_ibfk_1",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "ProductCoupons_ibfk_2",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TypeProduct",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TypeName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ImagePath = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TypeProd__3214EC07CE9A1883", x => x.Id);
                    table.ForeignKey(
                        name: "TypeProduct_ibdk_1",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    TypeProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Total = table.Column<double>(type: "float", nullable: true),
                    PaymentMethod = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    DeliveryId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bill__3214EC07EE0AA73E", x => x.Id);
                    table.ForeignKey(
                        name: "Bill_DeliveryType_Id_fk",
                        column: x => x.DeliveryId,
                        principalTable: "DeliveryType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Bill_PaymentMethod_Id_fk",
                        column: x => x.PaymentMethod,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Bill_TypeProduct_Id_fk",
                        column: x => x.TypeProductId,
                        principalTable: "TypeProduct",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CartId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TypeProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SizeType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CartItem__3214EC07183B77B9", x => x.Id);
                    table.ForeignKey(
                        name: "CartItem_TypeProduct_Id_fk",
                        column: x => x.TypeProductId,
                        principalTable: "TypeProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "CartItem_ibfk_2",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    TypeProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Material = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Color = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    LikeDetail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comment__3214EC07BA036D6D", x => x.Id);
                    table.ForeignKey(
                        name: "Comment_TypeProduct_Id_fk",
                        column: x => x.TypeProductId,
                        principalTable: "TypeProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    TypeProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SizeType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Size_pk", x => new { x.SizeType, x.TypeProductId });
                    table.ForeignKey(
                        name: "Size_TypeProduct_Id_fk",
                        column: x => x.TypeProductId,
                        principalTable: "TypeProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_DeliveryId",
                table: "Bill",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_PaymentMethod",
                table: "Bill",
                column: "PaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_TypeProductId",
                table: "Bill",
                column: "TypeProductId");

            migrationBuilder.CreateIndex(
                name: "customer_Id",
                table: "Cart",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_TypeProductId",
                table: "CartItem",
                column: "TypeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TypeProductId",
                table: "Comment",
                column: "TypeProductId");

            migrationBuilder.CreateIndex(
                name: "Receiver",
                table: "Delivery_INFO",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageURL_ProductId",
                table: "ImageURL",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SellerId",
                table: "Product",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "ProductId",
                table: "ProductCoupon",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Size_TypeProductId",
                table: "Size",
                column: "TypeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeProduct_ProductId",
                table: "TypeProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Bill");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Delivery_INFO");

            migrationBuilder.DropTable(
                name: "ImageURL");

            migrationBuilder.DropTable(
                name: "ProductCoupon");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DeliveryType");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "TypeProduct");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Seller");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
