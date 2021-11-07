using System;
using Route256.MerchandiseService.Domain.AggregationModels.MerchItemAggregationModel;
using Route256.MerchandiseService.Domain.AggregationModels.MerchPackAggregationModel;
using Route256.MerchandiseService.Domain.Exceptions.MerchPackItemAggregate;
using Xunit;

namespace Route256.MerchandiseService.Domain.Tests
{
    public class MerchItemAggregationTests
    {
        [Fact]
        public void ChangePackFillingPositiveResult()
        {
            //arrange
            var merchId = Guid.NewGuid();
            var merchPack = new MerchPack(
                new MerchPackName("test"),
                new MerchId[1]{new MerchId(merchId)}
            );

            var merchToAdd = new MerchId[] { new MerchId(Guid.NewGuid()), new MerchId(Guid.NewGuid()) };
            
            //act
            merchPack.SetPackFilling(merchToAdd, merchPack.PackName);
            
            //assert
            Assert.Equal(2, merchPack.PackFilling.Count);
        }
        
        [Fact]
        public void ChangePackFillingExceptionResult()
        {
            //arrange
            var merchId = Guid.NewGuid();
            var merchPack = new MerchPack(
                new MerchPackName("test"),
                new MerchId[1]{new MerchId(merchId)}
            );

            var merchToAdd = new MerchId[] { new MerchId(merchId)};
            
            //act
            
            
            //assert
            Assert.Throws<TheSamePackFillingException>(() => merchPack.SetPackFilling(merchToAdd, merchPack.PackName));
        }
    }
}