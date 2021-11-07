using System;
using System.Collections.Generic;
using System.Linq;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.Events;
using Route256.MerchandiseService.Domain.Exceptions.MerchPackItemAggregate;
using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel
{
    /// <summary>
    /// Сущность набора мерча
    /// Количество мерча в наборе начинается с 1 позиции
    /// </summary>
    public sealed class MerchPack : Entity
    {
        public MerchPack(MerchPackName merchPackName,
            IReadOnlyList<MerchId> merchPackFilling)
        {
            PackName = merchPackName;
            PackFilling = merchPackFilling;
        }
        public MerchPackName PackName { get; set; }
        public IReadOnlyList<MerchId> PackFilling { get; set; }

        /// <summary>
        /// Запрос на смену состава набора мерча
        /// </summary>
        /// <param name="packId"></param>
        /// <param name="merchPackFilling"></param>
        public void SetPackFilling(IReadOnlyList<MerchId> merchPackFilling, MerchPackName merchPackName)
        {
            if (merchPackFilling.All(x => PackFilling.All(y => y.Value == x.Value)))
            {
                throw new TheSamePackFillingException($"There is the same filling as the inner pack");
            }

            PackFilling = merchPackFilling;
            
            var setMerchPackFillingDomainEvent = new SetMerchPackFillingDomainEvent(merchPackName, merchPackFilling);

            this.AddDomainEvent(setMerchPackFillingDomainEvent);
        }
    }
}