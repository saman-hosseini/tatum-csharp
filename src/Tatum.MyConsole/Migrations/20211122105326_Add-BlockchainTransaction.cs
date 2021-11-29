using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TatumPlatform.MyConsole.Migrations
{
    public partial class AddBlockchainTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockchainTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NetworkId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    ParentBlockchainTransactionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockchainTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockchainTransaction_BlockchainTransaction_ParentBlockchainTransactionId",
                        column: x => x.ParentBlockchainTransactionId,
                        principalTable: "BlockchainTransaction",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockchainTransaction_ParentBlockchainTransactionId",
                table: "BlockchainTransaction",
                column: "ParentBlockchainTransactionId",
                unique: true,
                filter: "[ParentBlockchainTransactionId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockchainTransaction");
        }
    }
}
