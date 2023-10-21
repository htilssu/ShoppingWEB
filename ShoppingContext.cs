using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Models;

namespace ShoppingWEB;

public partial class ShoppingContext : IdentityDbContext<UserModel, RoleModel, string>
{
    public ShoppingContext(DbContextOptions<ShoppingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<DeliveryInfo> DeliveryInfos { get; set; }

    public virtual DbSet<DeliveryProvider> DeliveryProviders { get; set; }

    public virtual DbSet<ImageUrl> ImageUrls { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PaymentInfo> PaymentInfos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("name=ConnectionStrings:Mysql");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Cart");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
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

            entity.HasIndex(e => e.ProductId, "ImageURL_Products_id_fk");

            entity.Property(e => e.ImagePath).HasColumnName("image_path");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Thumnail).HasColumnName("thumnail");

            entity.HasOne(d => d.Product).WithMany(p => p.ImageUrls)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ImageURL_Products_id_fk");
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

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscountPercent).HasColumnName("discount_percent");
            entity.Property(e => e.InStock).HasColumnName("in_stock");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductDescription).HasColumnName("product_description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.Published).HasColumnName("published");
            entity.Property(e => e.ShortDescription)
                .HasColumnType("text")
                .HasColumnName("short_description");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("Products_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}