using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchPackRequestAggregate;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Infrastructure.Commands.GiveOutMerchPackRequest;

namespace Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    
    internal class ExtraditeMerchPackRequestCommandHandler : IRequestHandler<GiveOutMerchPackRequestCommand>
    {
        private readonly IMerchPackAggregationRepository _merchPackAggregationRepository;
        private readonly IExtraditeMerchPackAggregationRepository _extraditeMerchPackAggregationRepository;

        public ExtraditeMerchPackRequestCommandHandler(IMerchPackAggregationRepository merchPackAggregationRepository,
            IExtraditeMerchPackAggregationRepository extraditeMerchPackAggregationRepository)
        {
            _merchPackAggregationRepository = merchPackAggregationRepository;
            _extraditeMerchPackAggregationRepository = extraditeMerchPackAggregationRepository;
        }

        /// <summary>
        /// Handler запроса на выдачу набора мерча сотруднику
        /// </summary>
        public async Task<Unit> Handle(GiveOutMerchPackRequestCommand request, CancellationToken cancellationToken)
        {
            var merchPack = await _merchPackAggregationRepository.GetByNameAsync(request.MerchPackName, cancellationToken);
            var extraditePackRequest = new ExtraditeMerchPackRequest(RequestStatus.InWork, merchPack.PackName, request.EmployeeId);
            await _extraditeMerchPackAggregationRepository.CreateAsync(extraditePackRequest, cancellationToken);
            return Unit.Value;
        }
    }
}