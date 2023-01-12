using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLibrary;
using static DataLibrary.BusinessLogic.BookProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class MembersBookController : ApiController
    {
        //// GET: api/MembersBook
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/MembersBook/5
        public List<DataLibrary.Models.BookModel> Get(int id, string searchPhrase)
        {
            return GetMembersBooks(id, searchPhrase);
        }

        //// POST: api/MembersBook
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/MembersBook/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/MembersBook/5
        //public void Delete(int id)
        //{
        //}
    }
}
