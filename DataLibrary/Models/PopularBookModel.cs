using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class PopularBookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Yazar { get; set; }
        public double OrtalamaPuan { get; set; }
        public string TeslimTarihi { get; set; }

    }
}
