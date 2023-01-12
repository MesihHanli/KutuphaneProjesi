using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataLibrary.BusinessLogic.EmployeeProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        //// GET: api/Employee
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Employee/5
        public List<DataLibrary.Modles.EmployeeModel> Get(string tc)
        {
            return GetEmployee(tc);
        }

        //// POST: api/Employee
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Employee/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Employee/5
        //public void Delete(int id)
        //{
        //}
    }
}
