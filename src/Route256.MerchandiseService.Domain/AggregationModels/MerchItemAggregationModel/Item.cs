using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel
{
    public class Item : Entity
    {
        public ItemType Type { get; }

        public Item(ItemType type)
        {
            Type = type;
        }
    }
}