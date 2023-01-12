using KutuphaneWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataLibrary.BusinessLogic.MemberProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class SignUpMemberController : ApiController
    {
        //// GET: api/SignUpMember
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/SignUpMember/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/SignUpMember
        public int Post([FromBody]MemberModel value)
        {
            return CreateMember(value.AdSoyad, value.Telefon, value.Eposta, value.Parola);
        }

        //// PUT: api/SignUpMember/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/SignUpMember/5
        //public void Delete(int id)
        //{
        //}
    }
}
