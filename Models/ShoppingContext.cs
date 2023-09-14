using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShoppingWEB.Models;

public partial class ShoppingContext : DbContext
{
    public ShoppingContext()
    {
    }

    public ShoppingContext(DbContextOptions<ShoppingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<DeliveryInfo> DeliveryInfos { get; set; }

    public virtual DbSet<DeliveryProvider> DeliveryProviders { get; set; }

    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual DbSet<ImageUrl> ImageUrls { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PaymentInfo> PaymentInfos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=20.2.137.1;Port=3306;Database=Shopping;Uid=htilssu;Pwd=htilssuu;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.BirthDay).HasMaxLength(6);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEnd).HasMaxLength(6);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PRIMARY");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey }).HasName("PRIMARY");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }).HasName("PRIMARY");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Cart");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("Cart_ibfk_1");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.CartId, "cart_id");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quanity).HasColumnName("quanity");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("CartItems_ibfk_2");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("CartItems_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("image_path");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .HasColumnName("code");
            entity.Property(e => e.CouponDescription)
                .HasMaxLength(255)
                .HasColumnName("coupon_description");
            entity.Property(e => e.DiscountValue).HasColumnName("discount_value");
            entity.Property(e => e.EndAt)
                .HasColumnType("datetime")
                .HasColumnName("end_at");
            entity.Property(e => e.Limited).HasColumnName("limited");
            entity.Property(e => e.StartAt)
                .HasColumnType("datetime")
                .HasColumnName("start_at");

            entity.HasMany(d => d.Products).WithMany(p => p.Coupons)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCoupon",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("ProductCoupons_ibfk_1"),
                    l => l.HasOne<Coupon>().WithMany()
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("ProductCoupons_ibfk_2"),
                    j =>
                    {
                        j.HasKey("CouponId", "ProductId").HasName("PRIMARY");
                        j.ToTable("ProductCoupons");
                        j.HasIndex(new[] { "ProductId" }, "product_id");
                        j.IndexerProperty<string>("CouponId").HasColumnName("coupon_id");
                        j.IndexerProperty<string>("ProductId").HasColumnName("product_id");
                    });
        });

        modelBuilder.Entity<DeliveryInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Delivery_INFO");

            entity.HasIndex(e => e.Receiver, "receiver");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.Default).HasColumnName("default");
            entity.Property(e => e.District)
                .HasMaxLength(255)
                .HasColumnName("district");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone_number");
            entity.Property(e => e.Receiver).HasColumnName("receiver");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .HasColumnName("street");
            entity.Property(e => e.Ward)
                .HasMaxLength(255)
                .HasColumnName("ward");

            entity.HasOne(d => d.ReceiverNavigation).WithMany(p => p.DeliveryInfos)
                .HasForeignKey(d => d.Receiver)
                .HasConstraintName("Delivery_INFO_ibfk_1");
        });

        modelBuilder.Entity<DeliveryProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("DeliveryProvider");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryproviderName)
                .HasMaxLength(255)
                .HasColumnName("deliveryprovider_name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__EFMigrationsHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<ImageUrl>(entity =>
        {
            entity.HasKey(e => new { e.ImagePath, e.ProductId }).HasName("PRIMARY");

            entity.ToTable("ImageURL");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.ImagePath).HasColumnName("image_path");
            entity.Property(e => e.ProductId)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("product_id");
            entity.Property(e => e.Thumnail).HasColumnName("thumnail");

            entity.HasOne(d => d.Product).WithMany(p => p.ImageUrls)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ImageURL_ibfk_1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.HasIndex(e => e.CouponId, "coupon_id");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.HasIndex(e => e.PaymentId, "payment_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CouponId).HasColumnName("coupon_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");

            entity.HasOne(d => d.Coupon).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("Order_ibfk_2");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("Order_ibfk_3");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("Order_ibfk_1");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("OrderItem");

            entity.HasIndex(e => e.DeliveryProviderId, "delivery_provider_id");

            entity.HasIndex(e => e.OrderId, "order_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryProviderId).HasColumnName("delivery_provider_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quanity).HasColumnName("quanity");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.DeliveryProvider).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.DeliveryProviderId)
                .HasConstraintName("OrderItem_ibfk_2");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("OrderItem_ibfk_1");
        });

        modelBuilder.Entity<PaymentInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("PaymentINFO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PaymentMethod)
                .HasColumnType("enum('VISA','MASTERCARD','BANKING')")
                .HasColumnName("payment_method");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.HasIndex(e => e.SellerId, "seller_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DicountPrice).HasColumnName("dicount_price");
            entity.Property(e => e.InStock).HasColumnName("in_stock");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(255)
                .HasColumnName("product_description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.Published).HasColumnName("published");
            entity.Property(e => e.SellerId).HasColumnName("seller_id");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(255)
                .HasColumnName("short_description");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("Products_ibfk_1");

            entity.HasOne(d => d.Seller).WithMany(p => p.Products)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("Products_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
