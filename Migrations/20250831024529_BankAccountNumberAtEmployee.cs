using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemManagmentEmployeeWebApi.Migrations
{
    /// <inheritdoc />
    public partial class BankAccountNumberAtEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                table: "Employees");
        }
    }
}
