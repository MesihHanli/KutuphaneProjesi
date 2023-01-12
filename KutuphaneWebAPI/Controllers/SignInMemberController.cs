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
    public class SignInMemberController : ApiController
    {
        // GET: api/SingInMember
        public List<DataLibrary.Models.MemberModel> Get()
        {
            return getMembers();
        }

        // GET: api/SingInMember/5
        public List<DataLibrary.Models.MemberModel> Get(string searchPhrase)
        {
            return searchMembers(searchPhrase);
        }

        // POST: api/SingInMember
        public int Post([FromBody]MemberModel value)
        {
            return LoginMember(value.Eposta, value.Parola);
        }

        //// PUT: api/SingInMember/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/SingInMember/5
        //public void Delete(int id)
        //{
        //}
    }
}
