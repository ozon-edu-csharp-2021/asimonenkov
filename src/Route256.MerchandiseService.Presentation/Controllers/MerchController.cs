using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Domain.Models;
using Route256.MerchandiseService.Infrastructure.Queries.GetMerchItemGiveOutInfoRequest;
using Route256.MerchandiseService.Infrastructure.Queries.GetMerchPackItemGiveOutInfoRequest;
using Route256.MerchandiseService.Presentation.Exceptions;
using Route256.MerchandiseService.Presentation.Models.Requests;
using Route256.MerchandiseService.Presentation.Models.Responses;

namespace Route256.MerchandiseService.Presentation.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    public class MerchController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public MerchController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("merch-item/give-out/")]
        public async Task<ActionResult<SendRequestToGiveOutMerchItemRequest>> RequestMerchItem(
            SendRequestToGiveOutMerchItemResponse sendRequestToReceive, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("merch-pack/give-out/")]
        public async Task<ActionResult<SendRequestToGiveOutMerchPackRequest>> RequestMerchPack(
            SendRequestToGiveOutMerchPackResponse sendRequestToReceive, CancellationToken token)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("merch-item/give-out/info")]
        public async Task<ActionResult<GetMerchItemGiveOutInfoResponse>> GetMerchItemGiveOutInfo(GetMerchItemGiveOutInfoRequest request, CancellationToken token)
        {
            try
            {
                var allMerchTypes = Enumeration.GetAll<ItemType>().ToList();
                var allClothingSizes = Enumeration.GetAll<ClothingSize>().ToList();
                var allColors = Enumeration.GetAll<Colour>().ToList();

                if (allMerchTypes.All(x => x.Name.ToLowerInvariant() != request.MerchItem.ItemType.ToLowerInvariant()))
                {
                    throw new MerchTypeNotExistsException("Merch with current type is not exist");
                }

                if (!String.IsNullOrEmpty(request.MerchItem.ClothingSize) &&
                    allClothingSizes.All(x => x.Name.ToLowerInvariant() != request.MerchItem.ClothingSize.ToLowerInvariant()))
                {
                    throw new ClothingSizeNotExistsException("current size is not exist");
                }

                if (allColors.All(x => x.Name.ToLowerInvariant() != request.MerchItem.Colour.ToLowerInvariant()))
                {
                    throw new ClothingSizeNotExistsException("Current colour is not exist");
                }

                var requestToHandler = new GetMerchItemExtraditionInfoRequestQuery()
                {
                    EmployeeId = new EmployeeId(Guid.Parse(request.EmployeeId)),
                    MerchItem = new MerchItem(new Item(allMerchTypes.First(x => x.Name.ToLowerInvariant() == request.MerchItem.ItemType.ToLowerInvariant())),
                        allClothingSizes.First(x => x.Name.ToLowerInvariant() == request.MerchItem.ClothingSize.ToLowerInvariant()),
                        allColors.First(x => x.Name.ToLowerInvariant() == request.MerchItem.Colour.ToLowerInvariant()))
                };
                var response = await _mediator.Send(requestToHandler);


                if (response.GiveOutDate != null)
                {
                    return new GetMerchItemGiveOutInfoResponse()
                    {
                        GiveOutDate = response.GiveOutDate.Value,
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

        [HttpGet]
        [Route("merch-group/give-out/info")]
        public async Task<ActionResult<GetMerchPackGiveOutInfoResponse>> GetMerchGroupGiveOutInfo(
            GetMerchPackGiveOutInfoRequest request,
            CancellationToken token)
        {
            try
            {
                var requestToHandler = new GetMerchPackExtraditionInfoRequestQuery()
                {
                    EmployeeId = new EmployeeId(Guid.Parse(request.EmployeeId)),
                    MerchPackName = new MerchPackName(request.MerchPackName)
                };

                var response = await _mediator.Send(requestToHandler);

                if (response.GiveOutDate is not null)
                {
                    return new GetMerchPackGiveOutInfoResponse()
                    {
                        HasGiveOut = response.HasGiveOut,
                        GiveOutDate = response.GiveOutDate.Value,
                        MerchItemsNeedToGiveOut = response.MerchNeedToGiveOut
                    };
                }

                return new GetMerchPackGiveOutInfoResponse()
                {
                    HasGiveOut = false
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