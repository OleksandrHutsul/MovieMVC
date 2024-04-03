﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movie.DAL.Context;

#nullable disable

namespace Movie.DAL.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20240403234809_deleteVirtualMethod")]
    partial class deleteVirtualMethod
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Movie.DAL.Entities.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("ParentCategoryId")
                        .HasMaxLength(200)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Movie.DAL.Entities.FilmCategories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int?>("FilmsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.HasIndex("FilmsId");

                    b.ToTable("FilmCategories");
                });

            modelBuilder.Entity("Movie.DAL.Entities.Films", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("Release")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("Movie.DAL.Entities.FilmCategories", b =>
                {
                    b.HasOne("Movie.DAL.Entities.Categories", null)
                        .WithMany("FilmCategories")
                        .HasForeignKey("CategoriesId");

                    b.HasOne("Movie.DAL.Entities.Films", null)
                        .WithMany("FilmCategories")
                        .HasForeignKey("FilmsId");
                });

            modelBuilder.Entity("Movie.DAL.Entities.Categories", b =>
                {
                    b.Navigation("FilmCategories");
                });

            modelBuilder.Entity("Movie.DAL.Entities.Films", b =>
                {
                    b.Navigation("FilmCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
