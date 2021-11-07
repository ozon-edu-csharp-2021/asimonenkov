using System;
using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel
{
    /// <summary>
    /// Сущность мерча, как уникальной единцицы
    /// </summary>
    public sealed class MerchItem : Entity
    {
        /// <summary>
        /// Тип мерча
        /// </summary>
        public Item ItemType { get; }

        /// <summary>
        /// Размер одежды, в случае, если тип мерча - элемент одежды
        /// </summary>
        public ClothingSize ClothingSize { get; set; }

        /// <summary>
        /// Цвет мерча
        /// </summary>
        public Colour Colour { get; set; }

        /// <summary>
        /// Идентификатор мерча
        /// </summary>
        public MerchId MerchId { get; private set; }

        public MerchItem(Item itemType,
            ClothingSize size,
            Colour colour)

        {
            ItemType = itemType;
            ClothingSize = size;
            Colour = colour;
            SetMerchItemId();
        }

        private void SetMerchItemId()
        {
            MerchId = new MerchId(Guid.NewGuid());
        }
    }
}