using GildedRoseKata;

namespace GildedRoseTests
{
    public class ItemsTest
    {
        const string UNKNOWN_ITEM = "foo";
        const string BRIE = "Aged Brie";

        [Fact]
        public void Test_SellInDecreases()
        {
            //--- Setup
            var item = new Item { Name = UNKNOWN_ITEM, SellIn = 1, Quality = 0 };
            var rose = new GildedRose(new List<Item> { item });

            //--- Action
            rose.UpdateQuality();

            //--- Assert
            Assert.Equal(0, item.SellIn);
        }

        [Fact]
        public void Test_QualityIsNeverNegative()
        {
            var item = new Item { Name = UNKNOWN_ITEM, SellIn = 1, Quality = 0 };
            var rose = new GildedRose(new List<Item> { item });
            rose.UpdateQuality();
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void Test_AgedBrieIncreasesInQualityWithAge()
        {
            var item = new Item { Name = BRIE, SellIn = 1, Quality = 0 };
            var rose = new GildedRose(new List<Item> { item });
            rose.UpdateQuality();
            Assert.Equal(1, item.Quality);
        }


        [Fact]
        public void Test_QualityIsNeverMoreThanFifty() 
        {
            var item = new Item { Name = BRIE, SellIn = 1, Quality = 50 };
            var rose = new GildedRose(new List<Item> { item });
            rose.UpdateQuality();
            Assert.Equal(50, item.Quality);
        }

        [Fact]
        public void Test_QualityDegradesTwiceAsFastAfterSellByDate()
        {
            var item = new Item { Name = UNKNOWN_ITEM, SellIn = 0, Quality = 10 };
            var rose = new GildedRose(new List<Item> { item });
            rose.UpdateQuality();
            Assert.Equal(8, item.Quality);
        }
    }
}