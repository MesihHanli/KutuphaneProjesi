using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Isim { get; set; }

        public string Yazar { get; set; }

        public string Tur { get; set; }

        public int Sayfa { get; set; }

        public string Durum { get; set; }
    }
}
