﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Social_Media.Data;

namespace Social_Media.Migrations.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220410105644_Initial6")]
    partial class Initial6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Social_Media.Data.Models.Entities.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreaterId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreaterId");

                    b.HasIndex("PostId")
                        .IsUnique();

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Social_Media.Data.Models.Entities.Massage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreaterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("Discription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("UsingChatId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreaterId");

                    b.HasIndex("UsingChatId");

                    b.ToTable("Massages");
                });

            modelBuilder.Entity("Social_Media.Data.Models.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreaterId")
                        .HasColumnType("text");

                    b.Property<string>("Discription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Liked")
                        .HasColumnType("integer");

                    b.Property<string>("RouteToPhoto")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreaterId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Social_Media.Data.Models.Entities_Identity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("PhotoProfileRoute")
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.Property<string>("FollowingUserId")
                        .HasColumnType("text");

                    b.Property<string>("UserFriendsId")
                        .HasColumnType("text");

                    b.HasKey("FollowingUserId", "UserFriendsId");

                    b.HasIndex("UserFriendsId");

                    b.ToTable("FriendsAndFollowingUser");
                });

            modelBuilder.Entity("Social_Media.Data.Models.Entities.Chat", b =>
                {
                    b.HasOne("Social_Media.Data.Models.Entities_Identity.User", "Creater")
                        .WithMany("OwnerChats")
                        .HasForeignKey("CreaterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Social_Media.Data.Models.Entities.Post", "Post")
                        .WithOne("UsingChat")
                        .HasForeignKey("Social_Media.Data.Models.Entities.Chat", "PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Creater");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Social_Media.Data.Models.Entities.Massage", b =>
                {
                    b.HasOne("Social_Media.Data.Models.Entities_Identity.User", "Creater")
                        .WithMany("Massages")
                        .HasForeignKey("CreaterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Social_Media.Data.Models.Entities.Chat", "UsingChat")
                        .WithMany("UserMassage")
                        .HasForeignKey("UsingChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Creater");

                    b.Navigation("UsingChat");
                });

            modelBuilder.Entity("Social_Media.Data.Models.Entities.Post", b =>
                {
                    b.HasOne("Social_Media.Data.Models.Entities_Identity.User", "Creater")
                        .WithMany("Posts")
                        .HasForeignKey("CreaterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Creater");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.HasOne("Social_Media.Data.Models.Entities_Identity.User", null)
                        .WithMany()
                        .HasForeignKey("FollowingUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Social_Media.Data.Models.Entities_Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserFriendsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Social_Media.Data.Models.Entities.Chat", b =>
                {
                    b.Navigation("UserMassage");
                });

            modelBuilder.Entity("Social_Media.Data.Models.Entities.Post", b =>
                {
                    b.Navigation("UsingChat");
                });

            modelBuilder.Entity("Social_Media.Data.Models.Entities_Identity.User", b =>
                {
                    b.Navigation("Massages");

                    b.Navigation("OwnerChats");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
