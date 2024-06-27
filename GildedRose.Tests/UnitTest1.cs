using GildedRoseKata;

namespace GildedRoseTests
{
    public class UnitTest1
    {
        const string REGULAR_ITEM = "Toast";
        const string BRIE = "Aged Brie";

        //- At the end of each day our system lowers both values for every item
        [Fact]
        public void Test_SystemLowersSellInDays()
        {
            //--- Setup
            var items = new List<Item> {
                new Item { Name = REGULAR_ITEM, SellIn = 10, Quality = 20 },
                new Item { Name = BRIE, SellIn = 2, Quality = 0 },
            };
            var rose = new GildedRose(items);

            //--- Action
            rose.UpdateQuality();

            //--- Verify
            Assert.Equal(9, items[0].SellIn);
            Assert.Equal(1, items[1].SellIn);
        }

        //- At the end of each day our system lowers both values for every item
        [Fact]
        public void Test_SystemLowersQuality()
        {
            //--- Setup
            var items = new List<Item> {
                new Item { Name = REGULAR_ITEM, SellIn = 10, Quality = 20 },
            };
            var rose = new GildedRose(items);

            //--- Action
            rose.UpdateQuality();

            //--- Verify
            Assert.Equal(19, items[0].Quality);
        }

        //- Once the sell by date has passed, `Quality` degrades twice as fast
        [Fact]
        public void Test_QualityDegredationIncreasesAfterSellByDate()
        {
            //--- Setup
            var items = new List<Item> {
                new Item { Name = REGULAR_ITEM, SellIn = 0, Quality = 20 },
            };
            var rose = new GildedRose(items);

            //--- Action
            rose.UpdateQuality();

            //--- Verify
            Assert.Equal(18, items[0].Quality);
        }

    }
}