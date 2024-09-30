using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.Data;
using ODataBookStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ODataBookStore.Controllers
{
    public class BooksController : ODataController
    {
        private BookStoreContext db;

        public BooksController(BookStoreContext context)
        {
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            if (context.Books.Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                    context.Presses.Add(b.Press);
                }
                context.SaveChanges();
            }
        }

        [EnableQuery(PageSize = 1)]
        public IActionResult Get()
        {
            return Ok(db.Books);
        }

        [EnableQuery]
        [HttpGet("{key:int}")]
        public IActionResult Get(int key, string version)
        {
            return Ok(db.Books.FirstOrDefault(c => c.Id == key));
        }

        [EnableQuery]
        public IActionResult Post([FromBody] Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return Created(book);
        }

        [EnableQuery]
        public IActionResult Delete([FromBody] int key)
        {
            Book b = db.Books.FirstOrDefault(c => c.Id == key);
            if (b == null)
            {
                return NotFound();
            }
            db.Books.Remove(b);
            db.SaveChanges();
            return Ok();
        }

        [EnableQuery]
        [HttpGet("search")]
        public IActionResult Get([FromQuery] string search)
        {
            IQueryable<Book> query = db.Books;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b => b.Title.Contains(search));
            }

            return Ok(query);
        }

        [EnableQuery]
        [HttpGet("authorBooks")]
        public async Task<IActionResult> GetBooksByAuthor([FromQuery] string author)
        {
            var query = db.Books.AsQueryable();

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author == author);
            }

            var result = query.Select(b => new
            {
                b.Title,
                b.ISBN,
                PressName = b.Press.Name,
                PressCategory = b.Press.Category
            });

            return Ok(await result.ToListAsync());
        }

        [EnableQuery]
        [HttpGet("cityBooksByPrice")]
        public async Task<IActionResult> GetBooksByCityAndPrice([FromQuery] string city, [FromQuery] decimal minPrice)
        {
            var query = db.Books.AsQueryable();

            query = query.Where(b => b.Location.City == city && b.Price >= minPrice);

            var result = query.Select(b => new
            {
                b.Title,
                b.ISBN,
                PressName = b.Press.Name
            });

            return Ok(await result.ToListAsync());
        }

        [EnableQuery]
        [HttpGet("searchBooksByTitle")]
        public async Task<IActionResult> SearchBooksByTitle([FromQuery] string keyword)
        {
            var query = db.Books.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(b => b.Title.Contains(keyword));
            }

            query = query.OrderBy(b => b.Price);

            var result = query.Select(b => new
            {
                b.Title,
                b.ISBN,
                PressName = b.Press.Name,
                PressCategory = b.Press.Category
            });

            return Ok(await result.ToListAsync());
        }

        [EnableQuery]
        [HttpGet("multiAuthorBooks")]
        public async Task<IActionResult> GetBooksByMultipleAuthors([FromQuery] string[] authors, [FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var query = db.Books.AsQueryable();

            if (authors != null && authors.Any())
            {
                query = query.Where(b => authors.Contains(b.Author));
            }

            query = query.Where(b => b.Price >= minPrice && b.Price <= maxPrice);

            var result = query.Select(b => new
            {
                b.Title,
                b.ISBN,
                b.Author,
                PressName = b.Press.Name,
                PressCategory = b.Press.Category
            });

            result = result.OrderBy(b => b.Author);

            return Ok(await result.ToListAsync());
        }
        [HttpGet]
        [Route("odata/Books/averagePriceByCity")]
        public IActionResult GetAveragePriceByCity([FromQuery] string city)
        {
            
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("City parameter is required.");
            }

            
            var books = DataSource.GetBooks();

            
            if (books == null || !books.Any())
            {
                return NotFound("No books found in the data source.");
            }

            
            var booksInCity = books.Where(book => book.Location.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();

            
            if (!booksInCity.Any())
            {
                return NotFound($"No books found in the specified city: {city}.");
            }

            
            var totalBooks = booksInCity.Count;
            var averagePrice = booksInCity.Average(book => book.Price);

            
            Console.WriteLine($"City: {city} | Total Books: {totalBooks} | Average Price: {averagePrice}");

            
            var result = new
            {
                City = city,
                TotalBooks = totalBooks,
                AveragePrice = averagePrice,
                BookTitles = booksInCity.Select(book => book.Title).ToList()
            };

            
            return Ok(result);
        }


    }
}
