using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Bildirim { get; set; }
        public int isActive { get; set; }

    }
}
