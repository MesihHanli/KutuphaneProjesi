using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KutuphaneWebAPI.Models
{
    public class BookModel
    {
        public string Isim { get; set; }

        public string Yazar { get; set; }

        public string Tur { get; set; }

        public int Sayfa { get; set; }

        public string Durum { get; set; }

    }
}