using Obligatorisk_Opgave_1;
namespace BookTest
{
    [TestClass]
    public class BookTest
    {

        private readonly Book _book = new() { Id = 1, Title = "Abe", Price = 1200 };
        private readonly Book _titleToShort = new() { Id = 2, Title = "A", Price = 12 };
        private readonly Book _titleNull = new() { Id = 3, Title = null, Price = 1 };
        private readonly Book _priceToLow = new() { Id = 4, Title = "En længere titel", Price = -1 };
        private readonly Book _priceToHigh = new() { Id = 5, Title = "Snøvs", Price = 1201 };

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("Book Id: 1 Title: Abe Price: 1200", _book.ToString());
        }

        [TestMethod]
        public void TitleTest()
        {
            _book.ValidateTitle();
            Assert.ThrowsException<ArgumentNullException>(() => _titleNull.ValidateTitle());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _titleToShort.ValidateTitle());
        }
        [TestMethod]
        public void PriceTest()
        {
            _book.ValidatePrice();
            _titleToShort.ValidatePrice();
            _titleNull.ValidatePrice();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _priceToLow.ValidatePrice());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _priceToHigh.ValidatePrice());

        }

        [TestMethod]
        public void ValidateTest()
        {
            _book.Validate();
        }
    }
}