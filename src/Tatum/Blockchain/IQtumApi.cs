using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IQtumApi
    {
        [Get("/v3/qtum/account/balance/{address}")]
        Task<QtumBalance> GetBalance(string address);
    }
}
