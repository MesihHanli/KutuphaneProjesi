using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataLibrary.BusinessLogic.BookMemberProcessor;
using DataLibrary.Models;

namespace KutuphaneWebAPI.Controllers
{
    public class DeadlineController : ApiController
    {
        // GET: api/Deadline
        public List<BookMemberModel> Get()
        {
            return GetCloseDeadline();
        }

        // GET: api/Deadline/5
        public bool Get(int id)
        {
            return ExtendDeadline(id);
        }

        //// POST: api/Deadline
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Deadline/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Deadline/5
        //public void Delete(int id)
        //{
        //}
    }
}
