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
    public class MembersPuanController : ApiController
    {
        // GET: api/MembersPuan
        public List<PopularBookModel> Get()
        {
            return GetPopularBooks();
        }

        // GET: api/MembersPuan/5
        public List<int> Get(int id)
        {
            return GetMembersPuan(id);
        }

        // POST: api/MembersPuan
        public int Post([FromBody] Models.BookMemberModel value)
        {
            return SetMembersPuan(value.MemberId, value.BookId, value.Puan);
        }

        //// PUT: api/MembersPuan/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/MembersPuan/5
        //public void Delete(int id)
        //{
        //}
    }
}
