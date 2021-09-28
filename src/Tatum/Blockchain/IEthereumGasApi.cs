using Refit;
using System.Threading.Tasks;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IEthereumGasApi
    {
        [Get("/json/ethgasAPI.json")]
        Task<GasPrice> GasPrice();
    }
}
