using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Obligatorisk_Opgave_1;

namespace BookTest
{
    [TestClass]
    public class BookRepositoryTest
    {

     
        private BooksRepository _bookRepo;

        
        
#region Test Methods
        [TestInitialize]
        public void Init()
        {
            _bookRepo = new BooksRepository();
 
            //_bookRepo.Add(new Book() { Title = "Fight Club", Price = 1200 });
            //_bookRepo.Add(new Book() { Title = "The Shining", Price = 599 });
            //_bookRepo.Add(new Book() { Title = "The Silence of the Lambs", Price = 150 });
            //_bookRepo.Add(new Book() { Title = "Farenheit 451", Price = 249 });
            //_bookRepo.Add(new Book() { Title = "Biblen", Price = 666 });
        }
      
        [TestMethod]

        public void GetTest()
        {
            IEnumerable<Book> books = _bookRepo.Get();
            Assert.AreEqual(5, books.Count());
            Assert.AreEqual(books.First().Title, "Fight Club");

            //Order By Title
            IEnumerable<Book> sortByTitle = _bookRepo.Get(orderBy: "title");
            Assert.AreEqual(sortByTitle.First().Title, "Biblen");

            IEnumerable<Book> sortByTitleDesc = _bookRepo.Get(orderBy: "title_desc");
            Assert.AreEqual(sortByTitleDesc.First().Title, "The Silence of the Lambs");

            //Order By Price
            IEnumerable<Book> sortByPrice = _bookRepo.Get(orderBy: "price");
            Assert.AreEqual(sortByPrice.First().Price, 150);

            IEnumerable<Book> sortByPriceDesc = _bookRepo.Get(orderBy: "price_desc");
            Assert.AreEqual(sortByPriceDesc.First().Price, 1200);

            Assert.ThrowsException<ArgumentException>(() => _bookRepo.Get(orderBy: "Garbles"));

            //Price Filter
            IEnumerable<Book> noPriceFilter = _bookRepo.Get(minPriceFilter: null, maxPriceFilter: null);
            Assert.AreEqual(noPriceFilter.Count(), 5);


            IEnumerable<Book> filterMinPrice = _bookRepo.Get(minPriceFilter: 599);
            Assert.AreEqual(filterMinPrice.Count(), 3);

            IEnumerable<Book> filterMaxPrice = _bookRepo.Get(maxPriceFilter: 249);
            Assert.AreEqual(filterMaxPrice.Count(), 2);

            IEnumerable<Book> minMaxPrice = _bookRepo.Get(minPriceFilter: 151, maxPriceFilter: 1199);
            Assert.AreEqual(minMaxPrice.Count(), 3);

            IEnumerable<Book> weirdMinMaxPrice = _bookRepo.Get(minPriceFilter: 1510, maxPriceFilter: -55);
            Assert.AreEqual(weirdMinMaxPrice.Count(), 0);

        }

        [TestMethod]

        public void GetByIdTest()
        {
            Assert.IsNotNull(_bookRepo.GetById(4));
            Assert.IsNull(_bookRepo.GetById(0));
        }

        [TestMethod]
        public void AddTest()
        {
            Book validBook = new() {Title = "Valid Title", Price = 123 };

            Book nullTitleBook = new() { Title = null, Price = 500 };
            Book shortTitleBook = new() { Title = "Ba", Price = 400 };
            Book priceToLow = new() { Title = "Another valid title", Price = -55 };
            Book priceToHigh = new() { Title = "Price too High", Price = 1201 };

            Assert.AreEqual(6, _bookRepo.Add(validBook).Id);
            Assert.AreEqual(6, _bookRepo.Get().Count());

            Assert.ThrowsException<ArgumentNullException>(() => _bookRepo.Add(nullTitleBook));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _bookRepo.Add(shortTitleBook));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _bookRepo.Add(priceToLow));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _bookRepo.Add(priceToHigh));
            
        }

        [TestMethod]
        public void RemoveTest() 
        {
            Assert.IsNull( _bookRepo.Remove(0));
            Assert.AreEqual(5, _bookRepo.Remove(5)?.Id);
            Assert.AreEqual(4, _bookRepo.Get().Count());
            Assert.IsNull( _bookRepo.Remove(5));
        }

        [TestMethod]
        public void UpdateTest()
        {
            Assert.AreEqual(5, _bookRepo.Get().Count());
            Book update = new() { Title = "Updated Title", Price = 456 };
            Assert.IsNull(_bookRepo.Update(500, update));
            Assert.AreEqual(1, _bookRepo.Update(1, update)?.Id);
            Assert.AreEqual("Updated Title", _bookRepo.GetById(1).Title);
            Assert.AreEqual(456, _bookRepo.GetById(1).Price);
            Assert.AreEqual(5, _bookRepo.Get().Count());
            Book invalidBook = new() { Title = "a", Price = 5000 };

            Assert.ThrowsException<ArgumentOutOfRangeException>( () => _bookRepo.Update(1, invalidBook));

        }

 
    }


    #endregion
}

