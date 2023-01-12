using KutuphaneWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLibrary;
using static DataLibrary.BusinessLogic.EmployeeProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class SignInEmployeeController : ApiController
    {
        //// GET: api/SignInEmployee
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/SignInEmployee/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/SignInEmployee
        public bool Post([FromBody]EmployeeModel value)
        {
            return LoginEmployee(value.TcNo, value.Parola);
        }

        //// PUT: api/SignInEmployee/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/SignInEmployee/5
        //public void Delete(int id)
        //{
        //}
    }
}
