using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchItemRequestAggregate;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Infrastructure.Models.Responses;
using Route256.MerchandiseService.Infrastructure.Queries.GetMerchItemGiveOutInfoRequest;

namespace Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class GetMerchItemExtraditionInfoRequestQueryHandler : IRequestHandler<GetMerchItemExtraditionInfoRequestQuery,
        GetMerchItemGiveOutInfoResponse>
    {
        private readonly IMerchItemAggregationRepository _merchItemAggregationRepository;
        private readonly IExtraditeMerchItemAggregationRepository _extraditeMerchItemAggregationRepository;

        public GetMerchItemExtraditionInfoRequestQueryHandler(
            IMerchItemAggregationRepository merchItemAggregationRepository,
            IExtraditeMerchItemAggregationRepository extraditeMerchItemAggregationRepository)
        {
            _merchItemAggregationRepository = merchItemAggregationRepository;
            _extraditeMerchItemAggregationRepository = extraditeMerchItemAggregationRepository;
        }

        /// <summary>
        /// Handler запроса на получение информации о выдаче мерча сотруднику
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetMerchItemGiveOutInfoResponse> Handle(GetMerchItemExtraditionInfoRequestQuery request,
            CancellationToken cancellationToken)
        {
            var merch = await _merchItemAggregationRepository.FindByProperties(request.MerchItem.ItemType,
                request.MerchItem.Colour, request.MerchItem?.ClothingSize, cancellationToken);
            
            var extraditionInfo = await _extraditeMerchItemAggregationRepository.FindByEmployeeIdAndMerchId(
                request.EmployeeId,
                merch.MerchId, cancellationToken);
            
            if (extraditionInfo.Count == 0)
            {
                return new GetMerchItemGiveOutInfoResponse()
                {
                    HasGiveOut = false
                };
            }

            return new GetMerchItemGiveOutInfoResponse()
            {
                GiveOutDate = extraditionInfo.Last().GiveOutDate.Value,
                HasGiveOut = true
            };
        }
    }
}