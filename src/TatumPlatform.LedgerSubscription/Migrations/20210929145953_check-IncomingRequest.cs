using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TatumPlatform.LedgerSubscription.Migrations
{
    public partial class checkIncomingRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomingRequest",
                columns: table => new
                {
                    TId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JsonData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingRequest", x => x.TId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomingRequest");
        }
    }
}
