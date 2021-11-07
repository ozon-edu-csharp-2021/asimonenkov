using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels;
using Route256.MerchandiseService.Domain.AggregationModels.ChangeMerchPackFillingRequest;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Infrastructure.Commands.ChangePackFillingRequest;

namespace Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    internal class ChangePackFillingRequestCommandHandler : IRequestHandler<ChangePackFillingRequestCommand>
    {
        private readonly IChangeMerchPackFillingRequestRepository _requestRepository;
        private readonly IMerchPackAggregationRepository _merchPackAggregationRepository;

        public ChangePackFillingRequestCommandHandler(IChangeMerchPackFillingRequestRepository requestRepository,
            IMerchPackAggregationRepository merchPackAggregationRepository)
        {
            _requestRepository = requestRepository;
            _merchPackAggregationRepository = merchPackAggregationRepository;
        }

        /// <summary>
        /// Handler апроса на замену состава мерча
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  async Task<Unit> Handle(ChangePackFillingRequestCommand request, CancellationToken cancellationToken)
        {
            var merchPack = await _merchPackAggregationRepository.GetByNameAsync(request.PackName, cancellationToken);
            var newMerchIds = request.MerchIds.Where(x => !merchPack.PackFilling.Select(x => x.Value).Contains(x.Value)).ToList(); //поиск id мерча, которых не было в старом наборе
            var changePackFillingRequest = new ChangeMerchPackFillingRequest(request.PackName, newMerchIds);


            merchPack.SetPackFilling(request.MerchIds, merchPack.PackName);
            await _requestRepository.CreateAsync(changePackFillingRequest, cancellationToken);
            await _merchPackAggregationRepository.SetMerchPackFillingAsync(merchPack.PackName, request.MerchIds);

            return Unit.Value;
        }
    }
}