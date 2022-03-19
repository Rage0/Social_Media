using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;

namespace Social_Media.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Massage> Massages { get; set; }

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


            base.OnModelCreating(modelBuilder);
        }

    }
}
