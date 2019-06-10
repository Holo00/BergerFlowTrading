﻿// <auto-generated />
using System;
using BergerFlowTrading.DataTier.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BergerFlowTrading.DataTier.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190608170617_Limit4")]
    partial class Limit4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BergerFlowTrading.Model.Exchange", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApiTimeoutMilliseconds");

                    b.Property<string>("ApiUrl");

                    b.Property<int?>("DelayBetweenCallMilliseonds");

                    b.Property<string>("FacadeClassName");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("RateLimitIntervalSeconds");

                    b.Property<int>("RateMaxQuantity");

                    b.Property<string>("WSS_Url");

                    b.HasKey("ID");

                    b.ToTable("Exchanges");
                });

            modelBuilder.Entity("BergerFlowTrading.Model.ExchangeCustomSettings", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Exchange_ID");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("ID");

                    b.HasIndex("Exchange_ID");

                    b.ToTable("ExchangeCustomSettings");
                });

            modelBuilder.Entity("BergerFlowTrading.Model.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BergerFlowTrading.Model.LimitArbitrageStrategy4Settings", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<decimal>("BaseCurrency_Share_Percentage")
                        .HasColumnType("decimal(18, 10)");

                    b.Property<int?>("Exchange_ID_1");

                    b.Property<int?>("Exchange_ID_2");

                    b.Property<bool>("ManagementBalanceON");

                    b.Property<decimal>("Max_Price")
                        .HasColumnType("decimal(18, 10)");

                    b.Property<decimal>("MinATRValue")
                        .HasColumnType("decimal(18, 10)");

                    b.Property<decimal>("Min_Price")
                        .HasColumnType("decimal(18, 10)");

                    b.Property<string>("Symbol")
                        .IsRequired();

                    b.Property<decimal>("USD_Value_To_Trade")
                        .HasColumnType("decimal(18, 10)");

                    b.Property<string>("User_ID")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("Exchange_ID_1");

                    b.HasIndex("Exchange_ID_2");

                    b.HasIndex("User_ID");

                    b.ToTable("LimitArbitrageStrategy4Settings");
                });

            modelBuilder.Entity("BergerFlowTrading.Model.UserExchangeSecret", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Api_ID")
                        .IsRequired();

                    b.Property<string>("Api_Secret")
                        .IsRequired();

                    b.Property<int>("Exchange_ID");

                    b.Property<string>("User_ID")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("Exchange_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("UserExchangeSecrets");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BergerFlowTrading.Model.ExchangeCustomSettings", b =>
                {
                    b.HasOne("BergerFlowTrading.Model.Exchange", "Exchange")
                        .WithMany("ExchangeCustomSettings")
                        .HasForeignKey("Exchange_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BergerFlowTrading.Model.LimitArbitrageStrategy4Settings", b =>
                {
                    b.HasOne("BergerFlowTrading.Model.Exchange", "Exchange_1")
                        .WithMany("LimitStrategy4Settings1")
                        .HasForeignKey("Exchange_ID_1");

                    b.HasOne("BergerFlowTrading.Model.Exchange", "Exchange_2")
                        .WithMany("LimitStrategy4Settings2")
                        .HasForeignKey("Exchange_ID_2");

                    b.HasOne("BergerFlowTrading.Model.Identity.AppUser", "User")
                        .WithMany("LimitStrategy4Settings")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BergerFlowTrading.Model.UserExchangeSecret", b =>
                {
                    b.HasOne("BergerFlowTrading.Model.Exchange", "Exchange")
                        .WithMany("UserExchangeSecrets")
                        .HasForeignKey("Exchange_ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BergerFlowTrading.Model.Identity.AppUser", "User")
                        .WithMany("UserExchangeSecrets")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BergerFlowTrading.Model.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BergerFlowTrading.Model.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BergerFlowTrading.Model.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BergerFlowTrading.Model.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
