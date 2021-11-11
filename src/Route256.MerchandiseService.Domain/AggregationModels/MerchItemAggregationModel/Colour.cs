using Route256.MerchandiseService.Domain.Models;

namespace Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel
{
    public sealed class Colour : Enumeration
    {
        public static Colour OzonColour = new(1, nameof(OzonColour));
        public static Colour Red = new(2, nameof(Red));
        public static Colour Green = new(3, nameof(Green));
        public static Colour Blue = new(4, nameof(Blue));
        public static Colour Black = new(5, nameof(Black));

        public Colour(int id, string name) : base(id, name)
        {
        }
    }
}