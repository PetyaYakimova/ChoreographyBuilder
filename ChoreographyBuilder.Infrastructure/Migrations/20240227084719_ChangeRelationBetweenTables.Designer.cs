﻿// <auto-generated />
using System;
using ChoreographyBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChoreographyBuilder.Infrastructure.Migrations
{
    [DbContext(typeof(ChoreographyBuilderDbContext))]
    [Migration("20240227084719_ChangeRelationBetweenTables")]
    partial class ChangeRelationBetweenTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.Figure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Figure Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsFavourite")
                        .HasColumnType("bit")
                        .HasComment("Figure Is Favourite");

                    b.Property<bool>("IsHighlight")
                        .HasColumnType("bit")
                        .HasComment("Figure Is Highlight");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("Figure Name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User Identifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Figures");

                    b.HasComment("Figures");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.FigureOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Figure Options Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BeatCounts")
                        .HasColumnType("int")
                        .HasComment("Figure Option Beat Counts");

                    b.Property<int>("DynamicsType")
                        .HasColumnType("int");

                    b.Property<int>("EndPositionId")
                        .HasColumnType("int")
                        .HasComment("Figure Options End Position Identifier");

                    b.Property<int>("FigureId")
                        .HasColumnType("int")
                        .HasComment("Figure Options Figure Identifier");

                    b.Property<int>("StartPositionId")
                        .HasColumnType("int")
                        .HasComment("Figure Options Start Position Identifier");

                    b.HasKey("Id");

                    b.HasIndex("EndPositionId");

                    b.HasIndex("FigureId");

                    b.HasIndex("StartPositionId");

                    b.ToTable("FigureOptions");

                    b.HasComment("Figure Options");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.FullChoreography", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Full Choreography Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Full Choreography Name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User Identifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FullChoreographies");

                    b.HasComment("Full Choreographies");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.FullChoreographyVerseChoreography", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Full Choreography Verse Choreography Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("FullChoreographyId")
                        .HasColumnType("int")
                        .HasComment("Full Choreogprahy Identifier");

                    b.Property<int>("VerseChoreographyId")
                        .HasColumnType("int")
                        .HasComment("Verse Choreography Identifier");

                    b.Property<int>("VerseChoreographyOrder")
                        .HasColumnType("int")
                        .HasComment("Verse Choreography Order in which it appears in this choreography");

                    b.HasKey("Id");

                    b.HasIndex("FullChoreographyId");

                    b.HasIndex("VerseChoreographyId");

                    b.ToTable("FullChoreographiesVerseChoreographies");

                    b.HasComment("Full Choreography Verse Choreographies");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Position Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("Position Is Active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Position Name");

                    b.HasKey("Id");

                    b.ToTable("Positions");

                    b.HasComment("Positions");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.VerseChoreography", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Verse Choreograhy Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Verse Chorography Name");

                    b.Property<int>("Score")
                        .HasColumnType("int")
                        .HasComment("Verse Choreography Score at the time of saving it");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User Identifier");

                    b.Property<int>("VerseTypeId")
                        .HasColumnType("int")
                        .HasComment("Verse Type Identifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VerseTypeId");

                    b.ToTable("VerseChoreographies");

                    b.HasComment("Verse Choreographies");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.VerseChoreographyFigure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Verse Choreograhy Figure Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("FigureOptionId")
                        .HasColumnType("int")
                        .HasComment("Figure Option Identifier");

                    b.Property<int>("FigureOrder")
                        .HasColumnType("int")
                        .HasComment("Figure Order in which it appears in this choreography");

                    b.Property<int>("VerseChoreographyId")
                        .HasColumnType("int")
                        .HasComment("Verse Choreography Identifier");

                    b.HasKey("Id");

                    b.HasIndex("FigureOptionId");

                    b.HasIndex("VerseChoreographyId");

                    b.ToTable("VerseChoreographiesFigures");

                    b.HasComment("Verse Choreography Figures");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.VerseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Verse Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BeatCounts")
                        .HasColumnType("int")
                        .HasComment("Verse Beat Counts");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasComment("Verse Is Active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Verse Name");

                    b.HasKey("Id");

                    b.ToTable("VerseTypes");

                    b.HasComment("Verse Types");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.Figure", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.FigureOption", b =>
                {
                    b.HasOne("ChoreographyBuilder.Infrastructure.Data.Models.Position", "EndPosition")
                        .WithMany("FiguresWithEndPosition")
                        .HasForeignKey("EndPositionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChoreographyBuilder.Infrastructure.Data.Models.Figure", "Figure")
                        .WithMany("FigureOptions")
                        .HasForeignKey("FigureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChoreographyBuilder.Infrastructure.Data.Models.Position", "StartPosition")
                        .WithMany("FiguresWithStartPosition")
                        .HasForeignKey("StartPositionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EndPosition");

                    b.Navigation("Figure");

                    b.Navigation("StartPosition");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.FullChoreography", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.FullChoreographyVerseChoreography", b =>
                {
                    b.HasOne("ChoreographyBuilder.Infrastructure.Data.Models.FullChoreography", "FullChoreography")
                        .WithMany("VerseChoreographies")
                        .HasForeignKey("FullChoreographyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChoreographyBuilder.Infrastructure.Data.Models.VerseChoreography", "VerseChoreography")
                        .WithMany("FullChoreographies")
                        .HasForeignKey("VerseChoreographyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FullChoreography");

                    b.Navigation("VerseChoreography");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.VerseChoreography", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChoreographyBuilder.Infrastructure.Data.Models.VerseType", "VerseType")
                        .WithMany("VerseChoreographies")
                        .HasForeignKey("VerseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("VerseType");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.VerseChoreographyFigure", b =>
                {
                    b.HasOne("ChoreographyBuilder.Infrastructure.Data.Models.FigureOption", "FigureOption")
                        .WithMany()
                        .HasForeignKey("FigureOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChoreographyBuilder.Infrastructure.Data.Models.VerseChoreography", "VerseChoreography")
                        .WithMany("Figures")
                        .HasForeignKey("VerseChoreographyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FigureOption");

                    b.Navigation("VerseChoreography");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.Figure", b =>
                {
                    b.Navigation("FigureOptions");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.FullChoreography", b =>
                {
                    b.Navigation("VerseChoreographies");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.Position", b =>
                {
                    b.Navigation("FiguresWithEndPosition");

                    b.Navigation("FiguresWithStartPosition");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.VerseChoreography", b =>
                {
                    b.Navigation("Figures");

                    b.Navigation("FullChoreographies");
                });

            modelBuilder.Entity("ChoreographyBuilder.Infrastructure.Data.Models.VerseType", b =>
                {
                    b.Navigation("VerseChoreographies");
                });
#pragma warning restore 612, 618
        }
    }
}
