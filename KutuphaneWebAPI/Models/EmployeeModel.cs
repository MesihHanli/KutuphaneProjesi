using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KutuphaneWebAPI.Models
{
    public class EmployeeModel
    {
        //public int Id { get; set; } = 0;
        public string Ad { get; set; } = "";
        public string Soyad { get; set; } = "";
        public Int64 TcNo { get; set; } = 0;
        public string Eposta { get; set; } = "";
        public string Parola { get; set; } = "";
    }
}