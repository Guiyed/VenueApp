﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VenueApp.Data;

namespace VenueApp.Migrations
{
    [DbContext(typeof(VenueAppDbContext))]
    [Migration("20190224043434_ProfilePic")]
    partial class ProfilePic
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VenueApp.Models.Booking", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<int>("EventID");

                    b.HasKey("UserID", "EventID");

                    b.HasIndex("EventID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("VenueApp.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryID");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<bool>("Protected");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Events");

                    b.HasData(
                        new { ID = 1, CategoryID = 1, Created = new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), Date = new DateTime(2019, 7, 28, 18, 35, 5, 0, DateTimeKind.Unspecified), Deleted = false, Description = "Description of Test Event 1.", Location = "Launchcode. Miami, Florida", Name = "Test Event 1", Price = 9.99, Protected = true },
                        new { ID = 2, CategoryID = 1, Created = new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), Date = new DateTime(2019, 2, 23, 0, 0, 0, 0, DateTimeKind.Local), Deleted = false, Description = "Description of Test Event 2.", Location = "Launchcode. Miami, Florida", Name = "Test Event 2", Price = 10.5, Protected = false },
                        new { ID = 3, CategoryID = 2, Created = new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), Date = new DateTime(2019, 3, 1, 18, 10, 0, 0, DateTimeKind.Unspecified), Deleted = false, Description = "Description of Music Event", Location = "Miami, Florida", Name = "Music Event", Price = 10.99, Protected = false },
                        new { ID = 4, CategoryID = 3, Created = new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), Date = new DateTime(2019, 3, 2, 17, 0, 0, 0, DateTimeKind.Unspecified), Deleted = false, Description = "Description of Art Event", Location = "Ft. Lauderdale, Florida", Name = "Art Event", Price = 20.49, Protected = false },
                        new { ID = 5, CategoryID = 4, Created = new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), Date = new DateTime(2019, 3, 5, 8, 15, 0, 0, DateTimeKind.Unspecified), Deleted = false, Description = "Description of Business Event", Location = "Miramar, Florida", Name = "Business Event", Price = 17.0, Protected = false },
                        new { ID = 6, CategoryID = 5, Created = new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), Date = new DateTime(2019, 3, 12, 12, 45, 0, 0, DateTimeKind.Unspecified), Deleted = false, Description = "Description of Party Event", Location = "Coral Gables, Florida", Name = "Party Event", Price = 90.0, Protected = false },
                        new { ID = 7, CategoryID = 6, Created = new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), Date = new DateTime(2019, 3, 28, 10, 25, 10, 0, DateTimeKind.Unspecified), Deleted = false, Description = "Description of Class Event", Location = "Kendall, Florida", Name = "Classes Event", Price = 35.0, Protected = false },
                        new { ID = 8, CategoryID = 7, Created = new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), Date = new DateTime(2019, 4, 15, 21, 27, 10, 0, DateTimeKind.Unspecified), Deleted = false, Description = "Description of Sport Event", Location = "Weston. Miami, Florida", Name = "Sport Event", Price = 49.98, Protected = false },
                        new { ID = 9, CategoryID = 8, Created = new DateTime(2019, 2, 23, 23, 34, 34, 637, DateTimeKind.Local), Date = new DateTime(2019, 8, 1, 18, 35, 30, 0, DateTimeKind.Unspecified), Deleted = false, Description = "Description of Food & Drink Event", Location = "Sawgrass, Florida", Name = "Food & Drink Event", Price = 12.0, Protected = false }
                    );
                });

            modelBuilder.Entity("VenueApp.Models.EventCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<bool>("Protected");

                    b.HasKey("ID");

                    b.ToTable("Categories");

                    b.HasData(
                        new { ID = 1, Deleted = false, Name = "none", Protected = true },
                        new { ID = 2, Deleted = false, Name = "Music", Protected = true },
                        new { ID = 3, Deleted = false, Name = "Arts", Protected = true },
                        new { ID = 4, Deleted = false, Name = "Business", Protected = true },
                        new { ID = 5, Deleted = false, Name = "Parties", Protected = true },
                        new { ID = 6, Deleted = false, Name = "Classes", Protected = true },
                        new { ID = 7, Deleted = false, Name = "Sports", Protected = true },
                        new { ID = 8, Deleted = false, Name = "Food & Drikns", Protected = true }
                    );
                });

            modelBuilder.Entity("VenueApp.Models.Membership", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<bool>("Protected");

                    b.HasKey("ID");

                    b.ToTable("Memberships");

                    b.HasData(
                        new { ID = 1, Deleted = false, Name = "none", Protected = true },
                        new { ID = 2, Deleted = false, Name = "Bronze", Protected = true },
                        new { ID = 3, Deleted = false, Name = "Silver", Protected = true },
                        new { ID = 4, Deleted = false, Name = "Gold", Protected = true }
                    );
                });

            modelBuilder.Entity("VenueApp.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Location");

                    b.Property<int>("MembershipID");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("ProfilePicture");

                    b.Property<bool>("Protected");

                    b.Property<int>("TypeID");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.HasIndex("MembershipID");

                    b.HasIndex("TypeID");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new { ID = 1, Birthday = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Created = new DateTime(2019, 2, 23, 23, 34, 34, 635, DateTimeKind.Local), Deleted = false, MembershipID = 1, Password = "password", ProfilePicture = "/images/Avatar3.svg", Protected = true, TypeID = 1, Username = "admin" },
                        new { ID = 2, Birthday = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Created = new DateTime(2019, 2, 23, 23, 34, 34, 636, DateTimeKind.Local), Deleted = false, MembershipID = 1, Password = "password", ProfilePicture = "/images/Avatar3.svg", Protected = true, TypeID = 2, Username = "user" }
                    );
                });

            modelBuilder.Entity("VenueApp.Models.UserType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<bool>("Protected");

                    b.HasKey("ID");

                    b.ToTable("Types");

                    b.HasData(
                        new { ID = 1, Deleted = false, Name = "admin", Protected = true },
                        new { ID = 2, Deleted = false, Name = "user", Protected = true }
                    );
                });

            modelBuilder.Entity("VenueApp.Models.Booking", b =>
                {
                    b.HasOne("VenueApp.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VenueApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VenueApp.Models.Event", b =>
                {
                    b.HasOne("VenueApp.Models.EventCategory", "Category")
                        .WithMany("Events")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VenueApp.Models.User", b =>
                {
                    b.HasOne("VenueApp.Models.Membership", "Membership")
                        .WithMany("Users")
                        .HasForeignKey("MembershipID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VenueApp.Models.UserType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}