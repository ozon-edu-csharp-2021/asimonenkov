using System;
using System.Collections.Generic;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.ChangeMerchPackFillingRequest
{
    /// <summary>
    /// Запрос на получение набора мерча
    /// </summary>
    public class ChangeMerchPackFillingRequest : Entity
    {
        public ChangeMerchPackFillingRequest(RequestNumber requestNumber,
            MerchPackName merchPackName,
            IReadOnlyList<MerchId> additionalItems)
        {
            RequestNumber = requestNumber;
            MerchPackName = merchPackName;
            AdditionalItems = additionalItems;
            SetChangeDate();
        }
        
        /// <summary>
        /// Номер запроса
        /// </summary>
        public RequestNumber RequestNumber { get; set; }

        /// <summary>
        /// Id набора, который необходимо выдать
        /// </summary>
        public MerchPackName MerchPackName { get; set; }
        
        /// <summary>
        /// Дата замены
        /// </summary>
        public ChangeDate ChangeDate { get; private set; }
        
        /// <summary>
        /// Список мерча, который был добавлен
        /// </summary>
        public IReadOnlyList<MerchId> AdditionalItems { get; set; }
        
        private void SetChangeDate()
        {
            ChangeDate = new ChangeDate(DateTime.Now);
        }
    }
}