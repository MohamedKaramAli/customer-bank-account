using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class createcustomerrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerId",
                table: "Accounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_CustomerId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
