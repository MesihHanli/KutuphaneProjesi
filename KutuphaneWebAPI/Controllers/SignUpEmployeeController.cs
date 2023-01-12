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
    public class SignUpEmployeeController : ApiController
    {
        //// GET: api/SignUpEmployee
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/SignUpEmployee/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/SignUpEmployee
        public int Post([FromBody]EmployeeModel value)
        {
            return CreateEmployee(value.Ad, value.Soyad, value.TcNo, value.Eposta, value.Parola);
        }

        //// PUT: api/SignUpEmployee/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/SignUpEmployee/5
        //public void Delete(int id)
        //{
        //}
    }
}
