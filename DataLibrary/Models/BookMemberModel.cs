using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class BookMemberModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }

        public int MemberId { get; set; }

        public int TeslimDurumu { get; set; } //0 => teslim edilmedi, 1 => teslim edildi

        public string TeslimTarihi { get; set; } // YYYY-MM-DD

        public int Puan { get; set; }
        public int OrtalamaPuan { get; set; }

        public string Yazar { get; set; }

    }
}
