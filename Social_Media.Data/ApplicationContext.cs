using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities_Identity;

namespace Social_Media.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Massage> Massages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PrivateChat> PrivateChats { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Massage>()
                        .ToTable("Massages");

            modelBuilder.Entity<Chat>()
                        .HasMany(chat => chat.UserMassage)
                        .WithOne(massage => massage.UsingChat)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                        .HasOne(post => post.UsingChat)
                        .WithOne(chat => chat.Post)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PrivateChat>()
                        .HasMany(privateChat => privateChat.Massages)
                        .WithOne(massage => massage.PrivateChat)
                        .OnDelete(DeleteBehavior.Cascade);
            #region Entity user

            modelBuilder.Entity<User>()
                        .HasMany(user => user.UserFriends)
                        .WithMany(user => user.FollowingUser)
                        .UsingEntity(entity => entity.ToTable("FriendsAndFollowingUser"));

            modelBuilder.Entity<User>()
                        .HasMany(user => user.Massages)
                        .WithOne(massage => massage.Creater)
                        .HasForeignKey(massage => massage.CreaterId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                        .HasMany(user => user.Posts)
                        .WithOne(post => post.Creater)
                        .HasForeignKey(post => post.CreaterId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                        .HasMany(user => user.OwnerChats)
                        .WithOne(chat => chat.Creater)
                        .HasForeignKey(chat => chat.CreaterId)
                        .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                        .HasMany(user => user.PrivateChats)
                        .WithMany(privateChat => privateChat.Members)
                        .UsingEntity(entity => entity.ToTable("MembersToPrivateChat"));
            #endregion
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
