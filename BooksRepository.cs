using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorisk_Opgave_1
{
    public class BooksRepository
    {
        //starter _nextId på 6 da jeg så at klassen "skal" indeholde mindst 5 objekter i listen lidt sent. Dårlig undskyldning men jeg er mega træt
        private int _nextId = 6;
        private readonly List<Book> _books = new List<Book>()
        {

            
            new Book() {Id = 1, Title = "Fight Club", Price = 1200},
            new Book() {Id = 2, Title = "The Shining", Price = 599},
            new Book() {Id = 3, Title = "The Silence of the Lambs", Price = 150},
            new Book() {Id = 4, Title = "Biblen", Price = 666},
            new Book() {Id = 5, Title = "Farenheit 451", Price = 249}
        };
        #region Methods

        #region Get
        //Filtrer by Price
        //orderBy Title eller Price
        public IEnumerable<Book> Get(int? minPriceFilter = null, int? maxPriceFilter = null, string? orderBy = null) 
        {
            IEnumerable<Book> listCopy = new List<Book>(_books);

            // if (minPriceFilter == null && maxPriceFilter == null - Tænk om dén her er nødvendig når du laver testen
            //Filtrering af Priser
            
            if (minPriceFilter != null && maxPriceFilter == null) 
            {
                listCopy = listCopy.Where(f => f.Price >= minPriceFilter);
            }
            if (maxPriceFilter != null && minPriceFilter == null) 
            {
                listCopy = listCopy.Where(f => f.Price <= maxPriceFilter);
            }
            if (minPriceFilter != null && maxPriceFilter != null)
            {
                listCopy = listCopy.Where(f => f.Price >= minPriceFilter && f.Price <= maxPriceFilter);
            }
            //Order by Price or Title
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch(orderBy) 
                {
                    case "title":
                    case "title_asc":
                        listCopy = listCopy.OrderBy(t => t.Title);
                        break;
                    case "title_desc":
                        listCopy = listCopy.OrderByDescending(t => t.Title);
                        break;
                    case "price":
                    case "price_asc":
                        listCopy = listCopy.OrderBy(p => p.Price);
                        break;
                    case "price_desc":
                        listCopy = listCopy.OrderByDescending(p => p.Price);
                        break;
                    default:
                        throw new ArgumentException("unknown sort request " + orderBy);
                        break;

                }
            }
            
            return listCopy;
        }
        #endregion

        public Book? GetById(int id)
        {
            return _books.Find(book => book.Id == id);
        }

        public Book Add(Book book)
        {
            book.Validate();
            book.Id = _nextId++;
            _books.Add(book);
            return book;
        }

        public Book? Remove(int id) 
        { 
            Book? book = GetById(id);
            if (book == null )
            {
                return null;
            }
            
            _books.Remove(book);
            return book;
        }

        public Book? Update(int id, Book book) 
        {
            book.Validate();
            Book? findBook = GetById(id);
            if (findBook == null)
            {
                return null;
            }
            findBook.Title = book.Title;
            findBook.Price = book.Price;
            return findBook;
        }

#endregion

    }
}
