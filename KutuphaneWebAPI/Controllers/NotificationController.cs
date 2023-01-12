using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KutuphaneWebAPI.Models;
using static DataLibrary.BusinessLogic.NotificationProcessor;

namespace KutuphaneWebAPI.Controllers
{
    public class NotificationController : ApiController
    {
        //// GET: api/Notification
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Notification/5
        public List<DataLibrary.Models.NotificationModel> Get(int id)
        {
            return GetNotifications(id);
        }

        // POST: api/Notification
        public bool Post([FromBody]NotificationModel value)
        {
            return InsertNotification(value.MemberId, value.Bildirim);
        }

        //// PUT: api/Notification/5
        //public bool Put(int id, [FromBody] int isActive)
        //{
        //    return UpdateNotification(id, isActive);
        //}

        // DELETE: api/Notification/5
        public bool Delete(int id)
        {
            return DeleteNotification(id);
        }
    }
}
