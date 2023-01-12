using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataLibrary.BusinessLogic.BookMemberProcessor;


namespace KutuphaneWebAPI.Controllers
{
    public class MembersActiveBooksController : ApiController
    {
        //// GET: api/MembersActiveBooks
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/MembersActiveBooks/5
        public List<DataLibrary.Models.BookMemberModel> Get(int id)
        {
            return GetMembersBook(id);
        }

        //// POST: api/MembersActiveBooks
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/MembersActiveBooks/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/MembersActiveBooks/5
        //public void Delete(int id)
        //{
        //}
    }
}
