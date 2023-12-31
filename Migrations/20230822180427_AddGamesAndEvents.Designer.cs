﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using roko_test.Data;

#nullable disable

namespace roko_test.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230822180427_AddGamesAndEvents")]
    partial class AddGamesAndEvents
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("roko_test.Entities.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("roko_test.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    // b.Property<int?>("GameId")
                    //     .HasColumnType("integer");

                    b.Property<int>("Player_OneId")
                        .HasColumnType("integer");

                    b.Property<int>("Player_TwoId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    // b.HasIndex("GameId");

                    b.HasIndex("Player_OneId");

                    b.HasIndex("Player_TwoId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("roko_test.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Club_AwayId")
                        .HasColumnType("integer");

                    b.Property<int>("Club_Away_Score")
                        .HasColumnType("integer");

                    b.Property<int>("Club_HomeId")
                        .HasColumnType("integer");

                    b.Property<int>("Club_Home_Score")
                        .HasColumnType("integer");

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Club_AwayId");

                    b.HasIndex("Club_HomeId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("roko_test.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClubId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("roko_test.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("roko_test.Entities.Club", b =>
                {
                    b.HasOne("roko_test.Entities.Tournament", null)
                        .WithMany("Clubs")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("roko_test.Entities.Event", b =>
                {
                    // b.HasOne("roko_test.Entities.Game", null)
                    //     .WithMany("Events")
                    //     .HasForeignKey("GameId");

                    b.HasOne("roko_test.Entities.Player", "Player_One")
                        .WithMany()
                        .HasForeignKey("Player_OneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("roko_test.Entities.Player", "Player_Two")
                        .WithMany()
                        .HasForeignKey("Player_TwoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player_One");

                    b.Navigation("Player_Two");
                });

            modelBuilder.Entity("roko_test.Entities.Game", b =>
                {
                    b.HasOne("roko_test.Entities.Club", "Club_Away")
                        .WithMany()
                        .HasForeignKey("Club_AwayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("roko_test.Entities.Club", "Club_Home")
                        .WithMany()
                        .HasForeignKey("Club_HomeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("roko_test.Entities.Tournament", "Tournament")
                        .WithMany("Games")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club_Away");

                    b.Navigation("Club_Home");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("roko_test.Entities.Player", b =>
                {
                    b.HasOne("roko_test.Entities.Club", "Club")
                        .WithMany("Players")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("roko_test.Entities.Club", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("roko_test.Entities.Game", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("roko_test.Entities.Tournament", b =>
                {
                    b.Navigation("Clubs");

                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
