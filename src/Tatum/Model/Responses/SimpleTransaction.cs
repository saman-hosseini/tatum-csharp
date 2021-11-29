using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Responses
{
    public class SimpleTransaction
    {
        public SimpleTransaction()
        {
            Inputs = new Dictionary<string, decimal>();
            Outputs = new Dictionary<string, decimal>();
        }
        public Dictionary<string, decimal> Inputs { get; set; }
        public Dictionary<string, decimal> Outputs { get; set; }
        public decimal Fee { get; set; }
    }
}
