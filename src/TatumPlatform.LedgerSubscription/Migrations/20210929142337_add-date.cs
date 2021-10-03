using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TatumPlatform.LedgerSubscription.Migrations
{
    public partial class adddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "TransactionInTheBlock",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "KmsFailedTx",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "KmsCompletedTx",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "CustomerTradeMatch",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "AccountPendingBlockchainTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "AccountIncomingBlockchainTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "TransactionInTheBlock");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "KmsFailedTx");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "KmsCompletedTx");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "CustomerTradeMatch");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "AccountPendingBlockchainTransaction");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "AccountIncomingBlockchainTransactions");
        }
    }
}
