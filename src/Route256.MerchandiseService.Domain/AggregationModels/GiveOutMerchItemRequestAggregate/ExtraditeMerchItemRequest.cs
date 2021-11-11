using System;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchItemRequestAggregate
{
    /// <summary>
    /// Запрос на получение набора мерча
    /// </summary>
    public sealed class ExtraditeMerchItemRequest : Entity
    {
        public ExtraditeMerchItemRequest(RequestNumber requestNumber,
            RequestStatus requestStatus,
            MerchId merchId,
            EmployeeId employeeId)
        {
            RequestNumber = requestNumber;
            RequestStatus = requestStatus;
            MerchId = merchId;
            EmployeeId = employeeId;
            SetExtraditionDate();
        }
        
        public ExtraditeMerchItemRequest(
            RequestStatus requestStatus,
            MerchId merchId,
            EmployeeId employeeId)
        {
            RequestStatus = requestStatus;
            MerchId = merchId;
            EmployeeId = employeeId;
            SetExtraditionDate();
        }
        
        /// <summary>
        /// Номер заявки
        /// </summary>
        public RequestNumber RequestNumber { get; set; }

        /// <summary>
        /// Статус заявки
        /// </summary>
        public RequestStatus RequestStatus { get; set; }
        
        /// <summary>
        /// Id набора, который необходимо выдать
        /// </summary>
        public MerchId MerchId { get; set; }
        
        /// <summary>
        /// Id сотрудника, которому выдается набор
        /// </summary>
        public EmployeeId EmployeeId { get; set; }
        
        /// <summary>
        /// Дата выдачи мерча
        /// </summary>
        public GiveOutDate GiveOutDate { get; set; }

        private void SetExtraditionDate()
        {
            GiveOutDate = new GiveOutDate(DateTime.Now);
        }
    }
}