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
    public class ChangePackFillingRequestCommandHandler : IRequestHandler<ChangePackFillingRequestCommand>
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
        public async Task<Unit> Handle(ChangePackFillingRequestCommand request, CancellationToken cancellationToken)
        {
            var merchPack = await _merchPackAggregationRepository.FindByNameAsync(request.PackName, cancellationToken);
            var newMerchIds = request.MerchIds.Where(x =>
                !merchPack.PackFilling.Select(x => new Guid(x.Value.ToString())).Contains(x.Value)).ToList(); //поиск id мерча, которых не было в старом наборе
            var changePackFillingRequest = new ChangeMerchPackFillingRequest(
                null, request.PackName, newMerchIds);


            merchPack.SetPackFilling(request.MerchIds);
            await _requestRepository.CreateAsync(changePackFillingRequest, cancellationToken);

            return Unit.Value;
        }
    }
}