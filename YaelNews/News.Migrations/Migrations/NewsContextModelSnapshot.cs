﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using News.DAL;

#nullable disable

namespace News.Migrations.Migrations
{
    [DbContext(typeof(NewsContext))]
    partial class NewsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("News.DbModel.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Image")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SourceID");

                    b.Property<Guid?>("SubjectId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SubjectID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("News.DbModel.Shape", b =>
                {
                    b.Property<int>("DDD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DDD"));

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<string>("Name2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("DDD");

                    b.ToTable("Shapes");
                });

            modelBuilder.Entity("News.DbModel.Source", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("BaseUrl")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("IconUrl")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("News.DbModel.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("ShowInMenu")
                        .HasColumnType("bit");

                    b.Property<bool>("ShowInNewItem")
                        .HasColumnType("bit");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("News.DbModel.Item", b =>
                {
                    b.HasOne("News.DbModel.Source", "Source")
                        .WithMany("Items")
                        .HasForeignKey("SourceId")
                        .IsRequired()
                        .HasConstraintName("FK_Items_Sources");

                    b.HasOne("News.DbModel.Subject", "Subject")
                        .WithMany("Items")
                        .HasForeignKey("SubjectId")
                        .HasConstraintName("FK_Items_Subjects");

                    b.Navigation("Source");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("News.DbModel.Source", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("News.DbModel.Subject", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
