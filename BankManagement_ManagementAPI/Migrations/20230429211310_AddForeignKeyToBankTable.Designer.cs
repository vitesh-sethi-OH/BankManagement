﻿// <auto-generated />
using System;
using BankManagement_ManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankManagement_ManagementAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230429211310_AddForeignKeyToBankTable")]
    partial class AddForeignKeyToBankTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankManagement_ManagementAPI.Models.Bank", b =>
                {
                    b.Property<int>("AccNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccNo"));

                    b.Property<int>("AadharCard")
                        .HasColumnType("int");

                    b.Property<string>("AccName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PanCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccNo");

                    b.ToTable("Banks");

                    b.HasData(
                        new
                        {
                            AccNo = 1,
                            AadharCard = 64848,
                            AccName = "vitesh",
                            AccType = "savings",
                            Address = "reikhjtgoiew",
                            CreatedDate = new DateTime(2023, 4, 30, 2, 43, 10, 166, DateTimeKind.Local).AddTicks(2089),
                            PanCard = "wert326y8"
                        });
                });

            modelBuilder.Entity("BankManagement_ManagementAPI.Models.BankLocker", b =>
                {
                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("AccountNumber");

                    b.HasIndex("BankId");

                    b.ToTable("AccountNumber");
                });

            modelBuilder.Entity("BankManagement_ManagementAPI.Models.BankLocker", b =>
                {
                    b.HasOne("BankManagement_ManagementAPI.Models.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });
#pragma warning restore 612, 618
        }
    }
}