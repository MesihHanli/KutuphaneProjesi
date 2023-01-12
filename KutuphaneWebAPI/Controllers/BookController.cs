using KutuphaneWebAPI.Models;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataLibrary.BusinessLogic.BookProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class BookController : ApiController
    {
        //// GET: api/Book
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Book/5
        public List<DataLibrary.Models.BookModel> Get(string searchPhrase)
        {
            return SearchBooks(searchPhrase);
        }

        // POST: api/Book
        public int Post([FromBody] Models.BookModel value)
        {
            return CreateBook(value.Isim, value.Yazar, value.Tur, value.Sayfa);
        }

        //// PUT: api/Book/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/Book/5
        public bool Delete(int id)
        {
            return DeleteBook(id);
        }
    }
}
