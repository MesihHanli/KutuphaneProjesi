using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLibrary;
using static DataLibrary.BusinessLogic.BookMemberProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class LateBookController : ApiController
    {
        // GET: api/LateBook
        public List<DataLibrary.Models.BookMemberModel> Get()
        {
            return GetLateBooks();
        }

        //// GET: api/LateBook/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/LateBook
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/LateBook/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/LateBook/5
        //public void Delete(int id)
        //{
        //}
    }
}
