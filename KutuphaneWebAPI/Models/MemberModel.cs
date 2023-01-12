using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KutuphaneWebAPI.Models
{
    public class MemberModel
    {
        public string AdSoyad { get; set; }
        public Int64 Telefon { get; set; }
        public string Eposta { get; set; }
        public string Parola { get; set; }
    }
}