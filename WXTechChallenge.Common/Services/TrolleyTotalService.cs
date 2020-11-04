using System.Threading.Tasks;
using WXTechChallenge.Common.ApiClients.Interfaces;
using WXTechChallenge.Common.Dtos.Request;
using WXTechChallenge.Common.Services.Interfaces;

namespace WXTechChallenge.Common.Services
{
    public class TrolleyTotalService : ITrolleyTotalService
    {
        private readonly IWooliesXApiClient _wooliesXApiClient;

        public TrolleyTotalService(IWooliesXApiClient wooliesXApiClient)
        {
            _wooliesXApiClient = wooliesXApiClient;
        }

        public async Task<decimal> GetTotal(TrolleyRequest trolleyRequest)
        {
            return await _wooliesXApiClient.GetTotal(trolleyRequest).ConfigureAwait(false);
        }
    }
}
