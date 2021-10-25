using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Route256.MerchandiseService.Grpc;

namespace Route256.MerchandiseService.Server.GrpcServices
{
    public class MerchApiGrpcService : MerchApiGrpc.MerchApiGrpcBase
    {
        public override async Task<RequestMerchResponse> RequestMerch(
            RequestMerchRequest model,
            ServerCallContext context)
        {
            throw new NotImplementedException();
        }
        
        public override async Task<GetMerchExtraditionInfoResponse> GetMerchExtraditionInfo(
            Empty model,
            ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}