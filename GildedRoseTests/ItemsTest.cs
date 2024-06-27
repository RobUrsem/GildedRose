using GildedRoseKata;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;

namespace GildedRoseTests
{
    public class ItemsTest
    {
        const string UNKNOWN_ITEM = "foo";
        const string BRIE = "Aged Brie";
        const string SULFURAS = "Sulfuras, Hand of Ragnaros";
        const string BACKSTAGE = "Backstage passes to a TAFKAL80ETC concert";

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
        public void Test_QualityDecreases()
        {
            var item = new Item { Name = UNKNOWN_ITEM, SellIn = 1, Quality = 2 };
            var rose = new GildedRose(new List<Item> { item });
            rose.UpdateQuality();
            Assert.Equal(1, item.Quality);
        }

        [Fact]
        public void Test_QualityDecreasesTwiceAsFastAfterSellByDate()
        {
            var item = new Item { Name = UNKNOWN_ITEM, SellIn = 0, Quality = 2 };
            var rose = new GildedRose(new List<Item> { item });
            rose.UpdateQuality();
            Assert.Equal(0, item.Quality);
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
        public void Test_DefaultQualityIsNeverMoreThanFifty()
        {
            var item = new Item { Name = UNKNOWN_ITEM, SellIn = 1, Quality = 51 };
            var rose = new GildedRose(new List<Item> { item });
            rose.UpdateQuality();
            Assert.Equal(50, item.Quality);
        }

        [Fact]
        public void Test_SulfurasSellInDoesntDecrease()
        {
            var item = new Item { Name = SULFURAS, SellIn = 1, Quality = 1 };
            var rose = new GildedRose(new List<Item> { item });
            rose.UpdateQuality();
            Assert.Equal(1, item.SellIn);
        }

        [Fact]
        public void Test_SulfurasQualityDoesntDecrease()
        {
            var item = new Item { Name = SULFURAS, SellIn = 1, Quality = 1 };
            var rose = new GildedRose(new List<Item> { item });
            rose.UpdateQuality();
            Assert.Equal(1, item.Quality);
        }

        //- "Backstage passes", like aged brie, increases in `Quality` as its `SellIn` value approaches;
        //- `Quality` increases by `2` when there are `10` days or less and by `3` when there are `5` days or less but
        //- `Quality` drops to `0` after the concert
        [Theory]
        [InlineData(10, 1, 3)]
        [InlineData(5, 1, 4)]
        [InlineData(0, 1, 0)]
        public void Test_BackStagePassesQualityBehavior(int sellIn, int initialQuality, int expectedQuality)
        {
            var item = new Item { Name = BACKSTAGE, SellIn = sellIn, Quality = initialQuality };
            var rose = new GildedRose(new List<Item> { item });
            rose.UpdateQuality();
            Assert.Equal(expectedQuality, item.Quality);
        }
    }
}