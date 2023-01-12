using KutuphaneWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataLibrary.BusinessLogic.BorrowProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class BorrowController : ApiController
    {
        //// GET: api/Borrow
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Borrow/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Borrow
        public bool Post([FromBody]BookMemberModel value)
        {
            return BorrowBook(value.BookId, value.MemberId);
        }

        // PUT: api/Borrow/5
        public bool Put(int id, [FromBody]BookMemberModel value)
        {
            return ReturnBook(id, value.TeslimDurumu);
        }

        //// DELETE: api/Borrow/5
        //public void Delete(int id)
        //{
        //}
    }
}
