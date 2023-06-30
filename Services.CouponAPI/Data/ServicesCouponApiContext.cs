using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Services.CouponAPI.Models;

namespace Services.CouponAPI.Data
{
    public class ServicesCouponApiContext : DbContext
    {
        public ServicesCouponApiContext (DbContextOptions<ServicesCouponApiContext> options)
            : base(options)
        {
        }

        public DbSet<Coupon> Coupon { get; set; } = default!;

        //override on model creation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                CouponCode = "10OFF",
                DiscountAmount = 10,
                MinAmount = 10,
            });
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                CouponCode = "20OFF",
                DiscountAmount = 20,
                MinAmount = 20,
            });
        }
    }
}
