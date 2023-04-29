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
    [Migration("20230424103249_AddBankTable")]
    partial class AddBankTable
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
                });
#pragma warning restore 612, 618
        }
    }
}