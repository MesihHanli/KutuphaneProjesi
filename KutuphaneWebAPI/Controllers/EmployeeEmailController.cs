using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataLibrary.BusinessLogic.EmployeeProcessor;


namespace KutuphaneWebAPI.Controllers
{
    public class EmployeeEmailController : ApiController
    {
        //// GET: api/EmployeeEmail
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/EmployeeEmail/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/EmployeeEmail
        public bool Post([FromBody] Models.EmployeeModel value)
        {
            return UpdateMail(value.Eposta, value.TcNo);
        }

        //// PUT: api/EmployeeEmail/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/EmployeeEmail/5
        //public void Delete(int id)
        //{
        //}
    }
}
