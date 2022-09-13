﻿// <auto-generated />
using System;
using CryptoGambling.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CryptoGambling.Data.Migrations
{
    [DbContext(typeof(DataUserContext))]
    partial class DataUserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CryptoGambling.Data.CashRegisters.CashRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("BtcEarnedBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("BtcSharedBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("DogeEarnedBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("DogeSharedBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("LtcEarnedBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("LtcSharedBalance")
                        .HasColumnType("decimal(18,8)");

                    b.HasKey("Id");

                    b.ToTable("CashRegister");
                });

            modelBuilder.Entity("CryptoGambling.Data.Funds.Deposite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,8)");

                    b.Property<int>("Curreny")
                        .HasColumnType("int");

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Deposites");
                });

            modelBuilder.Entity("CryptoGambling.Data.Funds.Withdrawal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,8)");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Withdrawals");
                });

            modelBuilder.Entity("CryptoGambling.Data.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferralLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferredBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CryptoGambling.Data.WalletsData.Wallets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BtcAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BtcBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("BtcReferredBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<string>("DogeAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DogeBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("DogeReferredBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<string>("LtcAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LtcBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("LtcReferredBalance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("CryptoGambling.Data.Funds.Deposite", b =>
                {
                    b.HasOne("CryptoGambling.Data.Users.User", "User")
                        .WithMany("Deposites")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoGambling.Data.Funds.Withdrawal", b =>
                {
                    b.HasOne("CryptoGambling.Data.Users.User", "User")
                        .WithMany("Withdrawals")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoGambling.Data.WalletsData.Wallets", b =>
                {
                    b.HasOne("CryptoGambling.Data.Users.User", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("CryptoGambling.Data.WalletsData.Wallets", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoGambling.Data.Users.User", b =>
                {
                    b.Navigation("Deposites");

                    b.Navigation("Wallet");

                    b.Navigation("Withdrawals");
                });
#pragma warning restore 612, 618
        }
    }
}
