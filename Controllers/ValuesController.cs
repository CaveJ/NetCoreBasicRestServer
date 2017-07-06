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
        private static readonly List<Book> _books = new List<Book>();
        private int _nextId = 1;

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _books.Select(book => JsonConvert.SerializeObject(book));
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int removed = _books.RemoveAll(b => b.ID == id);
            if (removed == 0)
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
    }
}
