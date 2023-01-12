using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KutuphaneWebAPI.Models
{
    public class UpdateMemberPassModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}