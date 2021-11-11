using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.ChangeMerchPackFillingRequest;
using Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchPackRequestAggregate;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Infrastructure.Models.Responses;
using Route256.MerchandiseService.Infrastructure.Queries.GetMerchPackItemGiveOutInfoRequest;

namespace Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    internal class GetMerchPackExtraditionInfoRequestQueryHandler : IRequestHandler<
        GetMerchPackExtraditionInfoRequestQuery,
        GetMerchPackGiveOutInfoResponse>
    {
        private readonly IExtraditeMerchPackAggregationRepository _extraditeMerchPackAggregationRepository;
        private readonly IMerchItemAggregationRepository _merchItemAggregationRepository;
        private readonly IChangeMerchPackFillingRequestRepository _changeMerchPackFillingRequestRepository;

        public GetMerchPackExtraditionInfoRequestQueryHandler(
            IExtraditeMerchPackAggregationRepository extraditeMerchPackAggregationRepository,
            IMerchItemAggregationRepository merchItemAggregationRepository,
            IChangeMerchPackFillingRequestRepository changeMerchPackFillingRequestRepository)
        {
            _extraditeMerchPackAggregationRepository = extraditeMerchPackAggregationRepository;
            _merchItemAggregationRepository = merchItemAggregationRepository;
            _changeMerchPackFillingRequestRepository = changeMerchPackFillingRequestRepository;
        }

        /// <summary>
        /// Handler запроса на получение информации о выдаче набора мерча сотруднику
        /// Если в наборе были изменение, и они произошли после того, как сотруднику выдали набор, в ответ передается мерч, который необходимо выдать дополнительно
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetMerchPackGiveOutInfoResponse> Handle(GetMerchPackExtraditionInfoRequestQuery request,
            CancellationToken cancellationToken)
        {
            var merchPackFillingChanges = await _changeMerchPackFillingRequestRepository.GetByNameAsync(request.MerchPackName, cancellationToken);
            var giveOutRequests = await _extraditeMerchPackAggregationRepository.GetByEmployeeIdAndMerchPackNameAsync(request.EmployeeId, request.MerchPackName, cancellationToken);
            var merchItemsToGiveOut = new List<MerchItem>();

            if (giveOutRequests.Count == 0)
            {
                return new GetMerchPackGiveOutInfoResponse()
                {
                    HasGiveOut = false
                };
            }

            if (giveOutRequests.Count > 0 && merchPackFillingChanges.Count > 0)
            {
                var merchPackFillingChange = merchPackFillingChanges.Last();
                var giveOutRequest = giveOutRequests.Last();

                if (giveOutRequest.GiveOutDate.Value < merchPackFillingChange.ChangeDate.Value)
                {
                    foreach (var merchId in merchPackFillingChange.AdditionalItems)
                    {
                        var merchItemToGiveOut = await _merchItemAggregationRepository.FindByIdAsync(merchId, cancellationToken);
                        merchItemsToGiveOut.Add(merchItemToGiveOut);
                    }
                }
            }

            return new GetMerchPackGiveOutInfoResponse()
            {
                GiveOutDate = giveOutRequests.Last().GiveOutDate.Value,
                HasGiveOut = true,
                MerchNeedToGiveOut = merchItemsToGiveOut
            };
        }
    }
}