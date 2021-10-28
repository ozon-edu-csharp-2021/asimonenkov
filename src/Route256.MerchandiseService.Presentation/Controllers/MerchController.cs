using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Route256.MerchandiseService.Grpc;

namespace Route256.MerchandiseService.Presentation.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    public class MerchController : ControllerBase
    {
        [HttpPost]
        [Route("request-merch")]
        public async Task<ActionResult<SendRequestToReceiveMerchResponse>> RequestMerch (SendRequestToReceiveMerchRequest sendRequestToReceive, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("merch-extradition-info")]
        public async Task<ActionResult<GetMerchExtraditionInfoResponse>> GetMerchExtraditionInfo(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}