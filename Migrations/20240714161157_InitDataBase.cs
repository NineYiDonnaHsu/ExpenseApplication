using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExpenseApplication.Migrations
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "Category", "Date", "Title" },
                values: new object[,]
                {
                    { 1L, 30.0m, "食", new DateTime(2024, 7, 12, 16, 0, 0, 0, DateTimeKind.Utc), "豆干" },
                    { 2L, 30.0m, "食", new DateTime(2023, 7, 14, 16, 0, 0, 0, DateTimeKind.Utc), "燙青菜" },
                    { 3L, 100.0m, "食", new DateTime(2024, 7, 12, 16, 0, 0, 0, DateTimeKind.Utc), "紅燒肉" },
                    { 4L, 999.0m, "衣", new DateTime(2024, 7, 13, 16, 0, 0, 0, DateTimeKind.Utc), "米色小外套" },
                    { 5L, 199.0m, "食", new DateTime(2024, 7, 12, 16, 0, 0, 0, DateTimeKind.Utc), "節瓜" },
                    { 6L, 120.0m, "行", new DateTime(2024, 7, 13, 16, 0, 0, 0, DateTimeKind.Utc), "IRent租車" },
                    { 7L, 12000.0m, "住", new DateTime(2024, 7, 13, 16, 0, 0, 0, DateTimeKind.Utc), "房租" },
                    { 8L, 1200.0m, "住", new DateTime(2024, 7, 13, 16, 0, 0, 0, DateTimeKind.Utc), "電費" },
                    { 9L, 205.0m, "住", new DateTime(2024, 7, 14, 16, 0, 0, 0, DateTimeKind.Utc), "水費" },
                    { 10L, 300.0m, "住", new DateTime(2024, 7, 14, 16, 0, 0, 0, DateTimeKind.Utc), "瓦斯費" },
                    { 11L, 500.0m, "住", new DateTime(2024, 7, 14, 16, 0, 0, 0, DateTimeKind.Utc), "網路費" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");
        }
    }
}
