using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Route256.MerchandiseService.HttpModels.Requests;
using Route256.MerchandiseService.HttpModels.Responses;

namespace Route256.MerchandiseService.HttpClient.Interfaces
{
    public interface IMerchHttpClient
    {
        Task<GetMerchExtraditionInfoResponse> GetMerchExtraditionInfo(CancellationToken token);
        Task<SendRequestToReceiveMerchResponse> RequestMerch(SendRequestToReceiveMerchRequest sendRequestToReceive, CancellationToken token);
    }
}