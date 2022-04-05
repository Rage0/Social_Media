using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.Data.Models.Entities_Identity;
using Microsoft.AspNetCore.Identity;

namespace Social_Media.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Massage> Massages { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
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

            modelBuilder.Entity<User>()
                        .HasMany(user => user.UserFriends)
                        .WithMany(user => user.FollowingUser)
                        .UsingEntity(entity => entity.ToTable("FriendsAndFollowingUser"));

            modelBuilder.Entity<User>()
                        .HasMany(user => user.MemberChats)
                        .WithMany(chat => chat.Members)
                        .UsingEntity(entity => entity.ToTable("MemberAndMemberChats"));

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


            base.OnModelCreating(modelBuilder);
        }

    }
}
