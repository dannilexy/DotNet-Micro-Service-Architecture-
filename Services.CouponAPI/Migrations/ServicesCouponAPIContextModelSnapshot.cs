﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.CouponAPI.Data;

#nullable disable

namespace Services.CouponAPI.Migrations
{
    [DbContext(typeof(ServicesCouponApiContext))]
    partial class ServicesCouponAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("Services.CouponAPI.Models.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("DiscountAmount")
                        .HasColumnType("REAL");

                    b.Property<int>("MinAmount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Coupon");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CouponCode = "10OFF",
                            DiscountAmount = 10.0,
                            MinAmount = 10
                        },
                        new
                        {
                            Id = 2,
                            CouponCode = "20OFF",
                            DiscountAmount = 20.0,
                            MinAmount = 20
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
