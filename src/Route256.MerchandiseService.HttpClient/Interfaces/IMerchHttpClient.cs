using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Route256.MerchandiseService.HttpClient.Models.Requests;
using Route256.MerchandiseService.HttpClient.Models.Responses;

namespace Route256.MerchandiseService.HttpClient.Interfaces
{
    public interface IMerchHttpClient
    {
        Task<GetMerchGiveOutInfoResponse> GetMerchExtraditionInfo(CancellationToken token);
        Task<SendRequestToReceiveMerchResponse> RequestMerch(SendRequestToReceiveMerchRequest sendRequestToReceive, CancellationToken token);
    }
}