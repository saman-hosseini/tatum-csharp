using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatumPlatform.LedgerSubscription.Model
{
    public class IncomingRequest : BaseEntity<long>
    {
        public string JsonData { get; set; }
    }
}
