using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class rezervation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rezervations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RezervationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervations", x => x.Id);
                });
        }

            
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervations");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("f0b479a7-18fb-4aba-9575-00f679d76906"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("20491031-03e1-448d-bb2e-303a9af77b7c"));

            migrationBuilder.AlterColumn<bool>(
                name: "ReturnStatus",
                table: "LoanTransactions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("90a99db4-b85f-4f83-8ce6-ce05ed598f5a"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "narch@kodlama.io", new byte[] { 69, 2, 36, 88, 6, 68, 183, 252, 15, 201, 90, 134, 58, 112, 78, 223, 33, 204, 168, 87, 62, 227, 255, 222, 134, 14, 87, 247, 199, 80, 18, 223, 51, 198, 18, 180, 191, 240, 88, 95, 66, 109, 30, 107, 87, 100, 133, 74, 176, 163, 162, 17, 62, 198, 177, 154, 101, 69, 98, 201, 38, 125, 18, 97 }, new byte[] { 70, 230, 136, 197, 27, 246, 201, 42, 180, 205, 29, 29, 132, 201, 34, 181, 157, 194, 123, 134, 16, 78, 168, 125, 147, 252, 152, 243, 154, 102, 222, 234, 180, 110, 214, 196, 225, 156, 119, 59, 173, 181, 237, 48, 11, 23, 118, 192, 54, 63, 120, 221, 175, 230, 224, 83, 209, 74, 191, 190, 133, 152, 33, 173, 112, 111, 36, 240, 226, 186, 29, 254, 26, 108, 34, 56, 110, 114, 228, 33, 83, 217, 143, 248, 223, 255, 253, 210, 13, 117, 207, 128, 46, 56, 234, 183, 89, 25, 162, 51, 201, 200, 244, 85, 181, 56, 123, 135, 135, 237, 97, 179, 210, 169, 221, 246, 231, 181, 141, 224, 139, 230, 174, 221, 86, 119, 130, 153 }, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("ee3692fb-7438-4114-9500-8a6d56205662"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("90a99db4-b85f-4f83-8ce6-ce05ed598f5a") });
        }
    }
}
