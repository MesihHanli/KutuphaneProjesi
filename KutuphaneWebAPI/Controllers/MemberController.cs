using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KutuphaneWebAPI.Models;
using DataLibrary.Models;
using static DataLibrary.BusinessLogic.MemberProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class MemberController : ApiController
    {
        //// GET: api/Member
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Member/5
        public List<DataLibrary.Models.MemberModel> Get(int id)
        {
            return GetMember(id);
        }

        //// POST: api/Member
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/Member/5
        public bool Put(string Eposta, [FromBody] UpdateMemberPassModel value)
        {
            return UpdateMemberPassword(Eposta, value.OldPassword, value.NewPassword);
        }

        //// DELETE: api/Member/5
        //public void Delete(int id)
        //{
        //}
    }
}
