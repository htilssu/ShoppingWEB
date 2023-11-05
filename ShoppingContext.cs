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

    public virtual required DbSet<Bill> Bills { get; set; }

    public virtual required DbSet<Cart> Carts { get; set; }

    public virtual required DbSet<CartItem> CartItems { get; set; }

    public virtual required DbSet<Category> Categories { get; set; }

    public virtual required DbSet<Coupon> Coupons { get; set; }

    public virtual required DbSet<DeliveryInfo> DeliveryInfos { get; set; }

    public virtual required DbSet<DeliveryProvider> DeliveryProviders { get; set; }

    public virtual required DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual required DbSet<ImageUrl> ImageUrls { get; set; }

    public virtual required DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual required DbSet<Product> Products { get; set; }

    public virtual required DbSet<Size> Sizes { get; set; }

    public virtual required DbSet<Seller> Sellers { get; set; }

    public virtual required DbSet<TypeProduct> TypeProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("name=ConnectionStrings:Mysql");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Bill");

            entity.HasIndex(e => e.PaymentMethod, "Bill_Bill_Id_fk");

            entity.HasIndex(e => e.ItemId, "Bill_CartItem_Id_fk");

            entity.HasOne(d => d.Item).WithMany(p => p.Bills)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Bill_CartItem_Id_fk");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.InversePaymentMethodNavigation)
                .HasForeignKey(d => d.PaymentMethod)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Bill_Bill_Id_fk");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Cart");

            entity.HasIndex(e => e.CustomerId, "customer_Id");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("CartItem");
            // entity.HasIndex(e => e.CartId, "CartId");
            // entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
            //     .HasForeignKey(d => d.CartId)
            //     .HasConstraintName("CartItem_ibfk_2");
            // entity.HasOne(d => d.TypeProduct).WithMany(p => p.CartItems)
            //     .HasForeignKey(d => d.TypeProductId)
            //     .HasConstraintName("CartItem_TypeProduct_Id_fk");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.ImagePath).HasMaxLength(255);
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.CouponDescription).HasMaxLength(255);
            entity.Property(e => e.EndAt).HasColumnType("datetime");
            entity.Property(e => e.StartAt).HasColumnType("datetime");

            entity.HasMany(d => d.Products).WithMany(p => p.Coupons)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCoupon",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("ProductCoupons_ibfk_1"),
                    l => l.HasOne<Coupon>().WithMany()
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("ProductCoupons_ibfk_2"),
                    j =>
                    {
                        j.HasKey("CouponId", "ProductId").HasName("PRIMARY");
                        j.ToTable("ProductCoupon");
                        j.HasIndex(new[] { "ProductId" }, "ProductId");
                    });
        });

        modelBuilder.Entity<DeliveryInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Delivery_INFO");

            entity.HasIndex(e => e.ReceiverId, "Receiver");

            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Default).HasColumnName("default");
            entity.Property(e => e.District).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
            entity.Property(e => e.Street).HasMaxLength(255);
            entity.Property(e => e.Ward).HasMaxLength(255);
        });

        modelBuilder.Entity<DeliveryProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("DeliveryProvIder");

            entity.Property(e => e.ProviderName).HasMaxLength(255);
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

            entity.HasIndex(e => e.ProductId, "ImageURL_Product_Id_fk");

            entity.HasOne(d => d.Product).WithMany(p => p.ImageUrls)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ImageURL_Product_Id_fk");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PaymentName).HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.HasIndex(e => e.CategoryId, "CategoryId");

            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.ShortDescription).HasColumnType("text");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("Product_ibfk_1");

            entity.HasOne(p => p.Seller).WithMany(c => c.Products)
                .HasForeignKey(d => d.CategoryId).HasConstraintName("Product_Seller_Id_fk");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => new { e.TypeProductId, e.SizeType }).HasName("PRIMARY");

            entity.ToTable("Size");
            entity.HasOne(s => s.TypeProduct)
                .WithMany(t => t.Sizes);
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(s => s.Id).HasName("PRIMARY");
            entity.ToTable("Seller");
            entity.Property(c => c.SellerName).HasMaxLength(255);
        });

        modelBuilder.Entity<TypeProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("TypeProduct");

            entity.HasIndex(e => e.ProductId, "TypeProduct_ibdk_1");

            entity.Property(e => e.ImagePath).HasMaxLength(255);
            entity.Property(e => e.TypeName).HasMaxLength(255);

            entity.HasOne(d => d.Product).WithMany(p => p.TypeProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TypeProduct_ibdk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}