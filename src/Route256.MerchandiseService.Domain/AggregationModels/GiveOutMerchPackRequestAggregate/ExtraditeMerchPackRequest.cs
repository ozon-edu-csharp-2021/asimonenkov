using System;
using Route256.MerchandiseService.Domain.AggregationModels.EmployeeAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.GiveOutMerchPackRequestAggregate
{
    /// <summary>
    /// Запрос на получение набора мерча
    /// </summary>
    public class ExtraditeMerchPackRequest : Entity
    {
        public ExtraditeMerchPackRequest(RequestNumber requestNumber,
            RequestStatus requestStatus,
            MerchPackName merchPackName,
            EmployeeId employeeId)
        {
            RequestNumber = requestNumber;
            RequestStatus = requestStatus;
            MerchPackName = merchPackName;
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
        public MerchPackName MerchPackName { get; set; }
        
        /// <summary>
        /// Id сотрудника, которому выдается набор
        /// </summary>
        public EmployeeId EmployeeId { get; set; }
        
        /// <summary>
        /// Дата выдачи набора
        /// </summary>
        public GiveOutDate GiveOutDate { get; private set; }
        
        private void SetExtraditionDate()
        {
            GiveOutDate = new GiveOutDate(DateTime.Now);
        }
    }
}