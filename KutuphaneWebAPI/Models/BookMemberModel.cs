using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KutuphaneWebAPI.Models
{
    public class BookMemberModel
    {
        public int BookId { get; set; }

        public int MemberId { get; set; }

        public int TeslimDurumu { get; set; } //0 => teslim edilmedi, 1 => teslim edildi

        public string TeslimTarihi { get; set; } // YYYY-MM-DD

        public int Puan { get; set; }
    }
}