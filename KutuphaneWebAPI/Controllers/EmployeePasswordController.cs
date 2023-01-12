using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataLibrary.BusinessLogic.EmployeeProcessor;


namespace KutuphaneWebAPI.Controllers
{
    public class EmployeePasswordController : ApiController
    {
        //// GET: api/EmployeePassword
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/EmployeePassword/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/EmployeePassword
        public bool Post([FromBody] Models.EmployeeModel value)
        {
            return UpdatePassword(value.Parola, value.TcNo);
        }

        //// PUT: api/EmployeePassword/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/EmployeePassword/5
        //public void Delete(int id)
        //{
        //}
    }
}
