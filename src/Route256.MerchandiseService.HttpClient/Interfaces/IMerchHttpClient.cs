using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Route256.MerchandiseService.Server.Models.Requests;
using Route256.MerchandiseService.Server.Models.Responses;

namespace Route256.MerchandiseService.HttpClient.Interfaces
{
    public interface IMerchHttpClient
    {
        Task<GetMerchExtraditionInfoResponse> GetMerchExtraditionInfo(CancellationToken token);
        Task<RequestMerchResponse> RequestMerch(RequestMerchRequest request, CancellationToken token);
    }
}