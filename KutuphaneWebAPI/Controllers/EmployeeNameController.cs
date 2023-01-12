using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataLibrary.BusinessLogic.EmployeeProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class EmployeeNameController : ApiController
    {
        //// GET: api/EmployeeName
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/EmployeeName/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/EmployeeName
        public bool Post([FromBody] Models.EmployeeModel value)
        {
            return UpdateName(value.Ad, value.Soyad, value.TcNo);
        }

        // PUT: api/EmployeeName/5
        //public bool Put(int id, [FromBody]Models.EmployeeModel value)
        //{
        //    return UpdateName(value.Ad, value.Soyad,value.TcNo);
        //}

        //// DELETE: api/EmployeeName/5
        //public void Delete(int id)
        //{
        //}
    }
}
