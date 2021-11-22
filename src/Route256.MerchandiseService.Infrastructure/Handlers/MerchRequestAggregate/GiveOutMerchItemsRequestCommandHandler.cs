using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchItemRequestAggregate;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Infrastructure.Commands.GiveOutMerchItemRequest;

namespace Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    
    internal class ExtraditeMerchItemsRequestCommandHandler : IRequestHandler<GiveOutMerchItemsRequestCommand>
    {
        private readonly IGiveOutMerchItemAggregationRepository _giveOutMerchItemAggregationRepository;
        private readonly IMerchItemAggregationRepository _merchItemAggregationRepository;

        public ExtraditeMerchItemsRequestCommandHandler(IGiveOutMerchItemAggregationRepository giveOutMerchItemAggregationRepository, IMerchItemAggregationRepository merchItemAggregationRepository)
        {
            _giveOutMerchItemAggregationRepository = giveOutMerchItemAggregationRepository;
            _merchItemAggregationRepository = merchItemAggregationRepository;
        }

        /// <summary>
        /// Handler запроса на выдачу мерча сотруднику
        /// </summary>
        /// <param name="itemsRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(GiveOutMerchItemsRequestCommand itemsRequest, CancellationToken cancellationToken)
        {
            foreach (var merchItem in itemsRequest.MerchItems)
            {
                var merch = await _merchItemAggregationRepository.GetByProperties(merchItem.ItemType, merchItem.Colour, merchItem?.ClothingSize, cancellationToken);
                await _giveOutMerchItemAggregationRepository.CreateAsync(new GiveOutMerchItemRequest(RequestStatus.InWork, merch.MerchId, itemsRequest.EmployeeId), cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}