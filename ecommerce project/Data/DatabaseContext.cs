using ecommerce_project.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_project.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // user 
            modelBuilder.Entity<User>()
                .HasOne<User_type>()
                .WithMany()
                .HasForeignKey(u => u.TypeId);

            modelBuilder.Entity<User>()
                .Property(user => user.CreatedAt).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
                .Property(user => user.ModifiedAt).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().Property(user => user.Status).HasDefaultValue(true);

            //product
            modelBuilder.Entity<Product>()
                .HasOne<Product_category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>().Property(product => product.Status).HasDefaultValue(true);

            modelBuilder.Entity<Product>()
.Property(pc => pc.ModifiedAt).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Product>()
    .Property(pc => pc.CreatedAt).HasDefaultValueSql("GETDATE()");

            // catagory
            modelBuilder.Entity<Product_category>().Property(pc => pc.Status).HasDefaultValue(true);

            modelBuilder.Entity<Product_category>()
    .Property(pc => pc.ModifiedAt).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Product_category>()
    .Property(pc => pc.CreatedAt).HasDefaultValueSql("GETDATE()");

            //discount

            modelBuilder.Entity<Discount>().Property(dis => dis.Status).HasDefaultValue(true);

            modelBuilder.Entity<Discount>()
    .Property(dis => dis.ModifiedAt).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Discount>()
    .Property(dis => dis.CreatedAt).HasDefaultValueSql("GETDATE()");

            // one to many discount has only one reqord od each reqord
            modelBuilder.Entity<Product>()
                .HasOne<Discount>()
                .WithMany()
                .HasForeignKey(p => p.DiscountId);


            // Order details
            modelBuilder.Entity<Order_details>()
.Property(od => od.ModifiedAt).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Order_details>()
    .Property(od => od.CreatedAt).HasDefaultValueSql("GETDATE()");




            // Order_items
            modelBuilder.Entity<Order_items>()
                .HasOne<Order_details>()
                .WithMany()
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<Order_items>()
               .HasOne<Product>()
               .WithMany()
               .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Order_details>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(od => od.UserId);


    
            modelBuilder.Entity<Order_items>()
.Property(oi => oi.ModifiedAt).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Order_items>()
    .Property(oi => oi.CreatedAt).HasDefaultValueSql("GETDATE()");



            // many to many for comments
            //  commnet
            //modelBuilder.Entity<Comment>()
            //    .HasOne(u => u.Users)
            //    .WithMany(user => user.Comments)
            //    .HasForeignKey(ui => ui.UserId);

            //modelBuilder.Entity<Comment>()
            //    .HasOne(p => p.Products)
            //    .WithMany(product => product.Comments)
            //    .HasForeignKey(pi => pi.ProductId);








    //        modelBuilder.Entity<Comment>()
    //            .Property(cc => cc.).HasDefaultValueSql("GETDATE()");

    //        modelBuilder.Entity<Comment>()
    //.Property(entity => entity.).HasDefaultValueSql("GETDATE()");

    //        modelBuilder.Entity<Comment>().Property(entity => entity.Status).HasDefaultValue(true);








            // many to many for comments
            //  rating
          //  modelBuilder.Entity<Rate>()
          //.HasOne(u => u.Users)
          //.WithMany(user => user.Rates)
          //.HasForeignKey(ui => ui.UserId);

            //modelBuilder.Entity<Rate>()
            //    .HasOne(p => p.Products)
            //    .WithMany(product => product.Rates)
            //    .HasForeignKey(pi => pi.ProductId);

            //cart_item
            modelBuilder.Entity<Cart_item>()
                .HasOne<Shopping_session>()
                .WithMany()
                .HasForeignKey(ct => ct.SessionId);

            modelBuilder.Entity<Cart_item>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(ct => ct.ProductId);


            modelBuilder.Entity<Cart_item>()
.Property(ci => ci.ModifiedAt).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Cart_item>()
    .Property(ci => ci.CreatedAt).HasDefaultValueSql("GETDATE()");


            //Shopping_session
            modelBuilder.Entity<Shopping_session>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(ss => ss.UserId);

            modelBuilder.Entity<Shopping_session>()
.Property(ss => ss.ModifiedAt).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Shopping_session>()
    .Property(ss => ss.CreatedAt).HasDefaultValueSql("GETDATE()");

        
        }




        public DbSet<Rate> Rates { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User_type> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart_item> CartItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order_items> OrderItems { get; set; }
        public DbSet<Order_details> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_category> ProductCategories { get; set; }
        public DbSet<Shopping_session> ShoppingSession { get; set; }


    }
}
