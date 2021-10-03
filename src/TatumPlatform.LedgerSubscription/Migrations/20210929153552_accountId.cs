using Microsoft.EntityFrameworkCore.Migrations;

namespace TatumPlatform.LedgerSubscription.Migrations
{
    public partial class accountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AccountPendingBlockchainTransaction",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AccountIncomingBlockchainTransactions",
                newName: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AccountPendingBlockchainTransaction",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AccountIncomingBlockchainTransactions",
                newName: "Id");
        }
    }
}
