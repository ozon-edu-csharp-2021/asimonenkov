using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Route256.MerchandiseService.HttpClient.Interfaces;
using Route256.MerchandiseService.HttpModels.Requests;
using Route256.MerchandiseService.HttpModels.Responses;

namespace Route256.MerchandiseService.HttpClient
{
    public class MerchHttpClient : IMerchHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public MerchHttpClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetMerchExtraditionInfoResponse> GetMerchExtraditionInfo(CancellationToken token)
        {
            using var response = await _httpClient.GetAsync("v1/api/merch/merch-extradition-info", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GetMerchExtraditionInfoResponse>(body);
        }

        public async Task<SendRequestToReceiveMerchResponse> RequestMerch(SendRequestToReceiveMerchRequest sendRequestToReceive, CancellationToken token)
        {
            var content = new StringContent(JsonSerializer.Serialize(sendRequestToReceive), Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync("v1/api/merch/request-merch", content, token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<SendRequestToReceiveMerchResponse>(body);
        }
    }
}