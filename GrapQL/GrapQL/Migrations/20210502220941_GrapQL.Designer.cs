﻿// <auto-generated />
using System;
using GrapQL.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GrapQL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210502220941_GrapQL")]
    partial class GrapQL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GrapQL.Model.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cf2d0b97-6ea6-4ce5-a014-d877b36a8308"),
                            Description = "Cash account for our users",
                            OwnerId = new Guid("6f60463e-9566-415b-a53b-ea6ad6d57cc1"),
                            Type = 0
                        },
                        new
                        {
                            Id = new Guid("e4a99be5-e43d-4973-a47f-5ef09802adb2"),
                            Description = "Savings account for our users",
                            OwnerId = new Guid("f50a280c-4503-4d5d-8c9c-e8c033c4cf69"),
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("c3b364e1-adaa-44f9-807f-34ec2e155f99"),
                            Description = "Income account for our users",
                            OwnerId = new Guid("f50a280c-4503-4d5d-8c9c-e8c033c4cf69"),
                            Type = 3
                        });
                });

            modelBuilder.Entity("GrapQL.Model.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Owners");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6f60463e-9566-415b-a53b-ea6ad6d57cc1"),
                            Address = "John Doe's address",
                            Name = "John Doe"
                        },
                        new
                        {
                            Id = new Guid("f50a280c-4503-4d5d-8c9c-e8c033c4cf69"),
                            Address = "Jane Doe's address",
                            Name = "Jane Doe"
                        });
                });

            modelBuilder.Entity("GrapQL.Model.Account", b =>
                {
                    b.HasOne("GrapQL.Model.Owner", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("GrapQL.Model.Owner", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}