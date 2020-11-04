using System;
using System.Threading.Tasks;
using WXTechChallenge.Common.Dtos.Request;
using WXTechChallenge.Common.Services.Interfaces;

namespace WXTechChallenge.Common.Services
{
    public class TrolleyTotalService : ITrolleyTotalService
    {
        public Task<decimal> GetTotal(TrolleyRequest trolleyRequest)
        {
            throw new NotImplementedException();
        }
    }
}
