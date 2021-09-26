using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Responses;

namespace Tatum.Blockchain
{
    public interface IQtumApi
    {
        [Get("/v3/qtum/account/balance/{address}")]
        Task<QtumBalance> GetBalance(string address);
    }
}
