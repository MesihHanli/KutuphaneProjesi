using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLibrary.Models;
using static DataLibrary.BusinessLogic.BookMemberProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class BookMemberController : ApiController
    {
        //// GET: api/BookMember
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/BookMember/5
        public List<MemberModel> Get(int bookId)
        {
            return GetMembersOnBook(bookId);
        }

        //// POST: api/BookMember
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/BookMember/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/BookMember/5
        //public void Delete(int id)
        //{
        //}
    }
}
