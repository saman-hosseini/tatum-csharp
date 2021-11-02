using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatumPlatform.LedgerSubscription.Model
{
    public class LedgerContext : DbContext
    {
        public DbSet<IncomingRequest> IncomingRequest { get; set; }
        public DbSet<AccountIncomingBlockchainTransaction> AccountIncomingBlockchainTransactions { get; set; }
        public DbSet<AccountPendingBlockchainTransaction> AccountPendingBlockchainTransaction { get; set; }
        public DbSet<CustomerTradeMatch> CustomerTradeMatch { get; set; }
        public DbSet<KmsCompletedTx> KmsCompletedTx { get; set; }
        public DbSet<KmsFailedTx> KmsFailedTx { get; set; }
        public DbSet<TransactionInTheBlock> TransactionInTheBlock { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=LedgerSubscriptionDBNew;User Id=sa;Password=aA123456");
            }
        }
    }
}
