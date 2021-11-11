using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel
{
    public sealed class MerchType : Enumeration
    {
        public static MerchType Shirt = new(1, nameof(Shirt));
        public static MerchType Sweatshirt = new(2, nameof(Sweatshirt));
        public static MerchType Notepad = new(3, nameof(Notepad));
        public static MerchType Bag = new(4, nameof(Bag));
        public static MerchType Pen = new(5, nameof(Pen));
        public static MerchType Socks = new(6, nameof(Socks));

        public MerchType(int id, string name) : base(id, name)
        {
        }
    }
}