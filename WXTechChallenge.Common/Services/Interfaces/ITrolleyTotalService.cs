using System.Threading.Tasks;
using WXTechChallenge.Common.Dtos.Request;

namespace WXTechChallenge.Common.Services.Interfaces
{
    public interface ITrolleyTotalService
    {
        Task<decimal> GetTotal(TrolleyRequest trolleyRequest);
    }
}
