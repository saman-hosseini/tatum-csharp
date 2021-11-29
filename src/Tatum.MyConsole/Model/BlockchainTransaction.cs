using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatumPlatform.MyConsole.Model
{
    public class BlockchainTransaction
    {
        public long Id { get; set; }
        public int? NetworkId { get; set; }
        public int CurrencyId { get; set; }
        public long? ParentBlockchainTransactionId { get; set; }
        public BlockchainTransaction ParentBlockchainTransaction { get; set; }
        public BlockchainTransaction ChildBlockchainTransaction { get; set; }

    }
}
