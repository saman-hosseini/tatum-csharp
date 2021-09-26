using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Responses;

namespace Tatum.Blockchain
{
    public interface IDogechainApi
    {
        //https://dogechain.info/api/v1/address/balance/DMr3fEiVrPWFpoCWS958zNtqgnFb7QWn9D
        [Get("/api/v1/address/balance/{address}")]
        Task<DogecoinBalance> GetBalance(string address);
    }
}
