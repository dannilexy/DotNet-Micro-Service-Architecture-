using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedingcoupon2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "Id",
                keyValue: 1,
                column: "MinAmount",
                value: 10);

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { 2, "20OFF", 20.0, 20 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Coupon",
                keyColumn: "Id",
                keyValue: 1,
                column: "MinAmount",
                value: 20);
        }
    }
}
