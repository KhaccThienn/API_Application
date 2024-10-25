﻿// <auto-generated />
using System;
using API_Application.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_Application.Migrations
{
    [DbContext(typeof(DbComicAppContext))]
    [Migration("20241016160405_v1")]
    partial class v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API_Application.Core.Models.Actor", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Avatar")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("avatar");

                    b.Property<DateOnly?>("Birthday")
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<DateOnly?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<DateOnly?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK__actor__3213E83F4FDCEA32");

                    b.ToTable("actor", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.Comic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Poster")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("poster");

                    b.Property<DateOnly?>("PublishedAt")
                        .HasColumnType("date")
                        .HasColumnName("published_at");

                    b.Property<double?>("Rating")
                        .HasColumnType("float")
                        .HasColumnName("rating");

                    b.Property<int?>("ReleaseYear")
                        .HasColumnType("int")
                        .HasColumnName("release_year");

                    b.Property<string>("Slug")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("slug");

                    b.Property<byte?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("title");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("type");

                    b.Property<DateOnly?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updated_at");

                    b.Property<int?>("View")
                        .HasColumnType("int")
                        .HasColumnName("view");

                    b.HasKey("Id")
                        .HasName("PK__comic__3213E83F429B0A55");

                    b.ToTable("comic", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.ComicActor", b =>
                {
                    b.Property<int?>("ComicId")
                        .HasColumnType("int")
                        .HasColumnName("comic_id");

                    b.Property<int?>("ActorId")
                        .HasColumnType("int")
                        .HasColumnName("actor_id");

                    b.HasKey("ComicId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("comic_actor", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.ComicDirector", b =>
                {
                    b.Property<int?>("ComicId")
                        .HasColumnType("int")
                        .HasColumnName("comic_id");

                    b.Property<int?>("DirectorId")
                        .HasColumnType("int")
                        .HasColumnName("director_id");

                    b.HasKey("ComicId", "DirectorId");

                    b.HasIndex("DirectorId");

                    b.ToTable("comic_director", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.ComicGenre", b =>
                {
                    b.Property<int?>("ComicId")
                        .HasColumnType("int")
                        .HasColumnName("comic_id");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("genre_id");

                    b.HasKey("ComicId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("comic_genre", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.Director", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Avatar")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("avatar");

                    b.Property<DateOnly?>("Birthday")
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<DateOnly?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<DateOnly?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK__director__3213E83F08CA054B");

                    b.ToTable("director", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ComicId")
                        .HasColumnType("int")
                        .HasColumnName("comic_id");

                    b.Property<DateOnly?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int")
                        .HasColumnName("display_order");

                    b.Property<DateOnly?>("PublishedAt")
                        .HasColumnType("date")
                        .HasColumnName("published_at");

                    b.Property<byte?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("title");

                    b.Property<DateOnly?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK__episode__3213E83F660786D9");

                    b.HasIndex("ComicId");

                    b.ToTable("episode", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.Genre", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateOnly?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Slug")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("slug");

                    b.Property<DateOnly?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK__genre__3213E83F0636D778");

                    b.ToTable("genre", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int")
                        .HasColumnName("display_order");

                    b.Property<int?>("EpisodeId")
                        .HasColumnType("int")
                        .HasColumnName("episode_id");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<DateOnly?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updated_at");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("PK__images__3213E83F7E5CB749");

                    b.HasIndex("EpisodeId");

                    b.ToTable("images", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.Review", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int?>("ComicId")
                        .HasColumnType("int")
                        .HasColumnName("comic_id");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateOnly?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<int?>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<DateOnly?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updated_at");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("PK__review__3213E83FD8773093");

                    b.HasIndex("ComicId");

                    b.HasIndex("UserId");

                    b.ToTable("review", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Avatar")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("avatar");

                    b.Property<DateOnly?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("role");

                    b.Property<DateOnly?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("PK__user__3213E83F98E23F8E");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("API_Application.Core.Models.ComicActor", b =>
                {
                    b.HasOne("API_Application.Core.Models.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__comic_act__actor__4F7CD00D");

                    b.HasOne("API_Application.Core.Models.Comic", "Comic")
                        .WithMany()
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__comic_act__comic__4E88ABD4");

                    b.Navigation("Actor");

                    b.Navigation("Comic");
                });

            modelBuilder.Entity("API_Application.Core.Models.ComicDirector", b =>
                {
                    b.HasOne("API_Application.Core.Models.Comic", "Comic")
                        .WithMany()
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__comic_dir__comic__5070F446");

                    b.HasOne("API_Application.Core.Models.Director", "Director")
                        .WithMany()
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__comic_dir__direc__5165187F");

                    b.Navigation("Comic");

                    b.Navigation("Director");
                });

            modelBuilder.Entity("API_Application.Core.Models.ComicGenre", b =>
                {
                    b.HasOne("API_Application.Core.Models.Comic", "Comic")
                        .WithMany()
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__comic_gen__comic__52593CB8");

                    b.HasOne("API_Application.Core.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__comic_gen__genre__534D60F1");

                    b.Navigation("Comic");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("API_Application.Core.Models.Episode", b =>
                {
                    b.HasOne("API_Application.Core.Models.Comic", "Comic")
                        .WithMany("Episodes")
                        .HasForeignKey("ComicId")
                        .HasConstraintName("FK__episode__comic_i__4AB81AF0");

                    b.Navigation("Comic");
                });

            modelBuilder.Entity("API_Application.Core.Models.Image", b =>
                {
                    b.HasOne("API_Application.Core.Models.Episode", "Episode")
                        .WithMany("Images")
                        .HasForeignKey("EpisodeId")
                        .HasConstraintName("FK__images__episode___4D94879B");

                    b.Navigation("Episode");
                });

            modelBuilder.Entity("API_Application.Core.Models.Review", b =>
                {
                    b.HasOne("API_Application.Core.Models.Comic", "Comic")
                        .WithMany("Reviews")
                        .HasForeignKey("ComicId")
                        .HasConstraintName("FK__review__comic_id__4CA06362");

                    b.HasOne("API_Application.Core.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__review__user_id__4BAC3F29");

                    b.Navigation("Comic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_Application.Core.Models.Comic", b =>
                {
                    b.Navigation("Episodes");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("API_Application.Core.Models.Episode", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("API_Application.Core.Models.User", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
