using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asset_loaning_api.Migrations
{
    /// <inheritdoc />
    public partial class Newdatabasecreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetDetails",
                columns: table => new
                {
                    assetid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    serialnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    occupied = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDetails", x => x.assetid);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    transactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    supervisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    studentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    transaction_date = table.Column<DateOnly>(type: "date", nullable: false),
                    returning_supervisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.transactionID);
                    table.ForeignKey(
                        name: "FK_transactions_AssetDetails_assetId",
                        column: x => x.assetId,
                        principalTable: "AssetDetails",
                        principalColumn: "assetid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transactions_UserDetails_returning_supervisorId",
                        column: x => x.returning_supervisorId,
                        principalTable: "UserDetails",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_transactions_UserDetails_studentID",
                        column: x => x.studentID,
                        principalTable: "UserDetails",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_transactions_UserDetails_supervisorId",
                        column: x => x.supervisorId,
                        principalTable: "UserDetails",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transactions_assetId",
                table: "transactions",
                column: "assetId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_returning_supervisorId",
                table: "transactions",
                column: "returning_supervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_studentID",
                table: "transactions",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_supervisorId",
                table: "transactions",
                column: "supervisorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "AssetDetails");

            migrationBuilder.DropTable(
                name: "UserDetails");
        }
    }
}
