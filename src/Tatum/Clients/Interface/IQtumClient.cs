using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface IQtumClient
    {
        Task<QtumBalance> GetBalance(string address);
    }
}
