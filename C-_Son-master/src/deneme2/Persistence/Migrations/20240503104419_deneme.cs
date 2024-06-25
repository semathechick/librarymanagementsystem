using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class deneme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {











            {


                migrationBuilder.CreateTable(
                    name: "LoanTransactions",
                    columns: table => new
                    {
                        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        ReturnStatus = table.Column<bool>(type: "bit", nullable: false),
                        ReturnTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                        DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_LoanTransactions", x => x.Id);
                        table.ForeignKey(
                            name: "FK_LoanTransactions_Books_BookId",
                            column: x => x.BookId,
                            principalTable: "Books",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_LoanTransactions_Members_MemberId",
                            column: x => x.MemberId,
                            principalTable: "Members",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_LoanTransactions_MemberId",
                    table: "LoanTransactions",
                    column: "MemberId");
            }
        }

            
   

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBooks");

            migrationBuilder.DropTable(
                name: "BookPublishers");

            migrationBuilder.DropTable(
                name: "CategoryBooks");

            migrationBuilder.DropTable(
                name: "EmailAuthenticators");

            migrationBuilder.DropTable(
                name: "LoanTransactions");

            migrationBuilder.DropTable(
                name: "OtpAuthenticators");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
