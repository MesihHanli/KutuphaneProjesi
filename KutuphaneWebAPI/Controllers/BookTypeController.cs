using KutuphaneWebAPI.Models;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataLibrary.BusinessLogic.BookTypeProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class BookTypeController : ApiController
    {
        // GET: api/BookType
        public List<BookTypeModel> Get()
        {
            return getBookTypes();
        }

        //// GET: api/BookType/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/BookType
        public bool Post([FromBody] Models.BookType value)
        {
            return addBookType(value.Tur);
        }

        //// PUT: api/BookType/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/BookType/5
        public bool Delete(int id)
        {
            return deleteBookType(id);
        }
    }
}
