using Microsoft.EntityFrameworkCore.Migrations;

namespace TatumPlatform.LedgerSubscription.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountIncomingBlockchainTransactions",
                columns: table => new
                {
                    TId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlockHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlockHeight = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountIncomingBlockchainTransactions", x => x.TId);
                });

            migrationBuilder.CreateTable(
                name: "AccountPendingBlockchainTransaction",
                columns: table => new
                {
                    TId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlockHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlockHeight = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPendingBlockchainTransaction", x => x.TId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTradeMatch",
                columns: table => new
                {
                    TId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pair = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency1AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency2AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrefeeAccountIdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMaker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiredWithoutMatch = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTradeMatch", x => x.TId);
                });

            migrationBuilder.CreateTable(
                name: "KmsCompletedTx",
                columns: table => new
                {
                    TId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KmsCompletedTx", x => x.TId);
                });

            migrationBuilder.CreateTable(
                name: "KmsFailedTx",
                columns: table => new
                {
                    TId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SignatureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KmsFailedTx", x => x.TId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionInTheBlock",
                columns: table => new
                {
                    TId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WithdrawalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlockHeight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionInTheBlock", x => x.TId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountIncomingBlockchainTransactions");

            migrationBuilder.DropTable(
                name: "AccountPendingBlockchainTransaction");

            migrationBuilder.DropTable(
                name: "CustomerTradeMatch");

            migrationBuilder.DropTable(
                name: "KmsCompletedTx");

            migrationBuilder.DropTable(
                name: "KmsFailedTx");

            migrationBuilder.DropTable(
                name: "TransactionInTheBlock");
        }
    }
}
