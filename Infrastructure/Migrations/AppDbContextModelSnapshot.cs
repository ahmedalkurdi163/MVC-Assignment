﻿// <auto-generated />
using System;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Application_Domain.Models.Attachment", b =>
                {
                    b.Property<int>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttachmentId"));

                    b.Property<int>("FileType")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AttachmentId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Application_Domain.Models.Languages", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LanguageId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Application_Domain.Models.TVShow", b =>
                {
                    b.Property<int>("TVShowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TVShowId"));

                    b.Property<int>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TVShowId");

                    b.HasIndex("AttachmentId");

                    b.ToTable("TVShows");
                });

            modelBuilder.Entity("Application_Domain.Models.TVShowLanguages", b =>
                {
                    b.Property<int>("TVShowLanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TVShowLanguageId"));

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LanguagesLanguageId")
                        .HasColumnType("int");

                    b.Property<int>("TVShowId")
                        .HasColumnType("int");

                    b.HasKey("TVShowLanguageId");

                    b.HasIndex("LanguagesLanguageId");

                    b.HasIndex("TVShowId");

                    b.ToTable("TVShowLanguages");
                });

            modelBuilder.Entity("Application_Domain.Models.TVShow", b =>
                {
                    b.HasOne("Application_Domain.Models.Attachment", "Attachment_img")
                        .WithMany()
                        .HasForeignKey("AttachmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attachment_img");
                });

            modelBuilder.Entity("Application_Domain.Models.TVShowLanguages", b =>
                {
                    b.HasOne("Application_Domain.Models.Languages", null)
                        .WithMany("TVShows")
                        .HasForeignKey("LanguagesLanguageId");

                    b.HasOne("Application_Domain.Models.TVShow", null)
                        .WithMany("languages")
                        .HasForeignKey("TVShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application_Domain.Models.Languages", b =>
                {
                    b.Navigation("TVShows");
                });

            modelBuilder.Entity("Application_Domain.Models.TVShow", b =>
                {
                    b.Navigation("languages");
                });
#pragma warning restore 612, 618
        }
    }
}
