using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShoppingWEB.Models;

public partial class ShoppingContext : IdentityDbContext<UserModel, RoleModel, string>
{
    public ShoppingContext()
    {
    }

    public ShoppingContext(DbContextOptions<ShoppingContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Bill> Bills { get; set; }
    
    public virtual DbSet<BillItem> BillItems { get; set; }
    
    public virtual DbSet<Cart> Carts { get; set; }
    
    public virtual DbSet<CartItem> CartItems { get; set; }
    
    public virtual DbSet<Category> Categories { get; set; }
    
    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<DeliveryInfo> DeliveryInfos { get; set; }
   
    public virtual DbSet<DeliveryType> DeliveryTypes { get; set; }
    
    public virtual DbSet<ImageUrl> ImageUrls { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }
   
    public virtual DbSet<TypeProduct> TypeProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=ConnectionStrings:SqlServer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bill__3214EC07EE0AA73E");

            entity.ToTable("Bill");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DeliveryId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Delivery).WithMany(p => p.Bills)
                .HasForeignKey(d => d.DeliveryId)
                .HasConstraintName("Bill_DeliveryType_Id_fk");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.PaymentMethod)
                .HasConstraintName("Bill_PaymentMethod_Id_fk");
            
        });

        modelBuilder.Entity<BillItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("BillItem");

            entity.HasIndex(e => e.BillId, "BillItem_Bill_Id_fk");

            entity.HasIndex(e => e.TypeProductId, "BillItem_TypeProduct_Id_fk");

            entity.Property(e => e.SizeName).HasMaxLength(20);

            entity.HasOne(d => d.Bill).WithMany(p => p.BillItems)
                .HasForeignKey(d => d.BillId)
                .HasConstraintName("BillItem_Bill_Id_fk");

            entity.HasOne(d => d.TypeProduct).WithMany(p => p.BillItems)
                .HasForeignKey(d => d.TypeProductId)
                .HasConstraintName("BillItem_TypeProduct_Id_fk");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC0759460FA6");

            entity.ToTable("Cart");

            entity.HasIndex(e => e.CustomerId, "customer_Id");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItem__3214EC07183B77B9");

            entity.ToTable("CartItem");

            entity.HasIndex(e => e.CartId, "CartId");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CartId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SizeType).HasMaxLength(255);
            entity.Property(e => e.TypeProductId)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CartItem_ibfk_2");

            entity.HasOne(d => d.TypeProduct).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.TypeProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CartItem_TypeProduct_Id_fk");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07BEECF95C");

            entity.ToTable("Category");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC07BA036D6D");

            entity.ToTable("Comment");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Content).HasMaxLength(1);
            entity.Property(e => e.LikeDetail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Material)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Time).HasPrecision(0);
            entity.Property(e => e.TypeProductId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.TypeProduct).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TypeProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Comment_TypeProduct_Id_fk");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coupons__3214EC07105E2D15");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CouponDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EndAt).HasPrecision(0);
            entity.Property(e => e.StartAt).HasPrecision(0);
        });

        modelBuilder.Entity<DeliveryInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3214EC07654CE19F");

            entity.ToTable("Delivery_INFO");

            entity.HasIndex(e => e.ReceiverId, "Receiver");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.District)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ReceiverId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Ward)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3214EC073B71C692");

            entity.ToTable("DeliveryType");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProviderName).HasMaxLength(255);
        });

        modelBuilder.Entity<ImageUrl>(entity =>
        {
            entity.HasKey(e => e.ImagePath).HasName("PK__ImageURL__67172E6694BF550B");

            entity.ToTable("ImageURL");

            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.ImageUrls)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ImageURL_Product_Id_fk");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC07A98C5F0B");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaymentName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC0772F3CFDE");

            entity.ToTable("Product");

            entity.HasIndex(e => e.CategoryId, "CategoryId");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CategoryId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MadeIn).HasMaxLength(50);
            entity.Property(e => e.Material).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.SellerId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Style).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Product_ibfk_1");

            entity.HasOne(d => d.Seller).WithMany(p => p.Products)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Product_Seller_Id_fk");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seller__3214EC07DC4BD694");

            entity.ToTable("Seller");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.JoinAt).HasColumnType("date");
            entity.Property(e => e.SellerName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => new { e.SizeType, e.TypeProductId }).HasName("Size_pk");

            entity.ToTable("Size");

            entity.Property(e => e.SizeType).HasMaxLength(8);
            entity.Property(e => e.TypeProductId)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.TypeProduct).WithMany(p => p.Sizes)
                .HasForeignKey(d => d.TypeProductId)
                .HasConstraintName("Size_TypeProduct_Id_fk");
        });

        modelBuilder.Entity<TypeProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TypeProd__3214EC07CE9A1883");

            entity.ToTable("TypeProduct");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.TypeProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TypeProduct_ibdk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}