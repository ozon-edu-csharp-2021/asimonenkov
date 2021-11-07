using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Infrastructure.Commands.CreateMerchPackRequest;

namespace Route256.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    internal class CreateMerchPackRequestCommandHandler : IRequestHandler<CreateMerchPackRequestCommand>
    {
        private readonly IMerchPackAggregationRepository _merchPackAggregationRepository;

        public CreateMerchPackRequestCommandHandler(IMerchPackAggregationRepository merchPackAggregationRepository)
        {
            _merchPackAggregationRepository = merchPackAggregationRepository;
        }

        /// <summary>
        /// Handler запроса на создание набора мерча
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(CreateMerchPackRequestCommand request, CancellationToken cancellationToken)
        {
            var merchPack = new MerchPack(request.PackName, request.MerchIds);

            await _merchPackAggregationRepository.CreateAsync(merchPack, cancellationToken);
            
            return Unit.Value;
        }
    }
}