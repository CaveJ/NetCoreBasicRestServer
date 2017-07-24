using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using librarysample.Model;
using Newtonsoft.Json;

namespace librarysample.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private static readonly List<Book> _books;

        static BooksController()
        {
            _books = new Book[] {
              new Book(){ Name= "Rumo & die Wunder im Dunkeln", ID= 1, Author="Walther Moers", ISBN="abc" },
              new Book(){Name="Die dreizehneinhalb Leben des Käpt'n Blaubär",ID=2,Author="Walther Moers",ISBN="abc"},
              new Book(){Name="Die Stadt der träumenden Bücher",ID=3,Author="Walther Moers",ISBN="fr"},
              new Book(){Name="Das Labyrinth der träumenden Bücher",ID=4,Author="Walther Moers",ISBN="wef"},
              new Book(){Name="Der Schrecksenmeister",ID=5,Author="Walther Moers",ISBN="sdfds"},
              new Book(){Name="Ensel und Krete",ID=6,Author="Walther Moers",ISBN="sdfds"}
            }.ToList();
        }

        private int _nextId = 7;

        [HttpGet]
        public IEnumerable<string> Get([FromQuery]string name)
        {
            if (name == null)
                return _books.Select(book => JsonConvert.SerializeObject(book));
            else
                return _books.Where(book => book.Name.Contains(name)).Select(book => JsonConvert.SerializeObject(book));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Book result = _books.Find(b => b.ID == id);
            if (result == null)
                return NotFound();
            return new OkObjectResult(result);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Name) || string.IsNullOrWhiteSpace(book.Author) || string.IsNullOrWhiteSpace(book.ISBN))
                return BadRequest();
            book.ID = _nextId;
            _nextId++;
            _books.Add(book);
            return new OkObjectResult(book);
        }


        // PUT api/values/5
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody]Book book)
        {
            // not yet implemented
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Name)
            || string.IsNullOrWhiteSpace(book.Author)
            || string.IsNullOrWhiteSpace(book.ISBN)
            || book.ID == -1)
                return BadRequest();

            Book b = _books.Find(element => element.ID == book.ID);
            if (b == null)
                return NotFound();

            b.Name = book.Name;
            b.Author = book.Author;
            b.ISBN = book.ISBN;

            return new ObjectResult(b);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_books.RemoveAll(b => b.ID == id) == 0)
                return NotFound();
            return Ok();
        }

        [HttpPost("fib")]
        public IActionResult Fibonacci([FromBody]int amount)
        {
            if (amount <= 0)
                return BadRequest();

            long result = 0;
            if (amount <= 2)
            {
                result = 1;
            }
            else
            {
                long prev = 1;
                for (int i = 0; i < amount; i++)
                {
                    long t = result;
                    result = result + prev;
                    prev = t;
                }
            }
            return new OkObjectResult(result);
        }

        [HttpPost("pi")]
        public IActionResult CalculatePiWithPrecision([FromBody]int digits)
        {
            if (digits <= 0)
                return BadRequest();

            string pi = CalculatePi(digits);
            return new OkObjectResult(pi);
        }

        // Found here: https://stackoverflow.com/questions/11677369/how-to-calculate-pi-to-n-number-of-places-in-c-sharp-using-loops
        // Who found it there: http://www.mathpropress.com/stan/bibliography/spigot.pdf
        private static string CalculatePi(int digits)
        {
            digits++;
            uint[] x = new uint[digits * 10 / 3 + 2];
            uint[] r = new uint[digits * 10 / 3 + 2];
            uint[] pi = new uint[digits];

            for (int j = 0; j < x.Length; j++)
                x[j] = 20;

            for (int i = 0; i < digits; i++)
            {
                uint carry = 0;
                for (int j = 0; j < x.Length; j++)
                {
                    uint num = (uint)(x.Length - j - 1);
                    uint dem = num * 2 + 1;
                    x[j] += carry;
                    uint q = x[j] / dem;
                    r[j] = x[j] % dem;
                    carry = q * num;
                }
                pi[i] = (x[x.Length - 1] / 10);
                r[x.Length - 1] = x[x.Length - 1] % 10; ;

                for (int j = 0; j < x.Length; j++)
                    x[j] = r[j] * 10;
            }
            var result = "";
            uint c = 0;
            for (int i = pi.Length - 1; i >= 0; i--)
            {
                pi[i] += c;
                c = pi[i] / 10;
                result = (pi[i] % 10).ToString() + result;
            }
            return result;
        }
    }
}
