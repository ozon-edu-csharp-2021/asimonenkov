using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Domain.Models;
using Route256.MerchandiseService.Grpc;
using Route256.MerchandiseService.Infrastructure.Queries.GetMerchItemGiveOutInfoRequest;
using Route256.MerchandiseService.Infrastructure.Queries.GetMerchPackItemGiveOutInfoRequest;
using Route256.MerchandiseService.Presentation.Exceptions;

namespace Route256.MerchandiseService.Presentation.GrpcServices
{
    public class MerchApiGrpcService : MerchApiGrpc.MerchApiGrpcBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public MerchApiGrpcService(IMediator mediator, ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        public override async Task<Empty> SendRequestToGiveOutMerchItem(
            SendRequestToGiveOutMerchItemRequest model,
            ServerCallContext context)
        {
            throw new NotImplementedException();
        }
        
        public override async Task<Empty> SendRequestToGiveOutMerchPack(
            SendRequestToGiveOutMerchPackRequest model,
            ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение информации о выдаче набора сотруднику
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GetPackGiveOutInfoResponse> GetPackGiveOutInfo(
            GetPackGiveOutInfoRequest model,
            ServerCallContext context)
        {
            try
            {
                var request = new GetMerchPackExtraditionInfoRequestQuery()
                {
                    EmployeeId = new EmployeeId(Guid.Parse(model.EmployeeId)),
                    MerchPackName = new MerchPackName(model.MerchPackName)
                };

                var response = await _mediator.Send(request);

                if (response.GiveOutDate is not null)
                {
                    return new GetPackGiveOutInfoResponse()
                    {
                        MerchNeedToGiveOut =
                        {
                            response.MerchNeedToGiveOut.Select(x => new MerchItemGrpc()
                            {
                                ClothingSize = x.ClothingSize.Name,
                                Colour = x.Colour.Name,
                                MerchType = x.ItemType.Type.Name
                            })
                        },
                        HasGiveOut = response.HasGiveOut,
                        GiveOutDate = Timestamp.FromDateTimeOffset(response.GiveOutDate.Value),
                    };
                }

                return new GetPackGiveOutInfoResponse()
                {
                    HasGiveOut = response.HasGiveOut
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Получение информации о выдаче мерча сотруднику
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GetMerchItemGiveOutInfoResponse> GetMerchItemGiveOutInfo(
            GetMerchItemGiveOutInfoRequest model,
            ServerCallContext context)
        {
            try
            {
                var allMerchTypes = Enumeration.GetAll<ItemType>().ToList();
                var allClothingSizes = Enumeration.GetAll<ClothingSize>().ToList();
                var allColors = Enumeration.GetAll<Colour>().ToList();

                if (allMerchTypes.All(x => x.Name.ToLowerInvariant() != model.MerchItem.MerchType.ToLowerInvariant()))
                {
                    throw new MerchTypeNotExistsException("Merch with current type is not exist");
                }

                if (!String.IsNullOrEmpty(model.MerchItem.ClothingSize) &&
                    allClothingSizes.All(x => x.Name.ToLowerInvariant() != model.MerchItem.ClothingSize.ToLowerInvariant()))
                {
                    throw new ClothingSizeNotExistsException("current size is not exist");
                }

                if (allColors.All(x => x.Name.ToLowerInvariant() != model.MerchItem.Colour.ToLowerInvariant()))
                {
                    throw new ClothingSizeNotExistsException("Current colour is not exist");
                }

                var request = new GetMerchItemExtraditionInfoRequestQuery()
                {
                    EmployeeId = new EmployeeId(Guid.Parse(model.EmployeeId)),
                    MerchItem = new MerchItem(new Item(allMerchTypes.First(x => x.Name.ToLowerInvariant() == model.MerchItem.MerchType.ToLowerInvariant())),
                        allClothingSizes.First(x => x.Name.ToLowerInvariant() == model.MerchItem.ClothingSize.ToLowerInvariant()),
                        allColors.First(x => x.Name.ToLowerInvariant() == model.MerchItem.Colour.ToLowerInvariant()))
                };
                var response = await _mediator.Send(request);


                if (response.GiveOutDate != null)
                {
                    return new GetMerchItemGiveOutInfoResponse()
                    {
                        GiveOutDate = Timestamp.FromDateTimeOffset(response.GiveOutDate.Value),
                        HasGiveOut = response.HasGiveOut
                    };
                }
                
                return new GetMerchItemGiveOutInfoResponse()
                {
                    HasGiveOut = response.HasGiveOut
                };
            }

            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}