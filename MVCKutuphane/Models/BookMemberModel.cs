using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MVCKutuphane.Models
{
    public class BookMemberModel
    {
        [Display(Name = "Kitap Id")]
        public int BookId { get; set; }

        [Display(Name = "Kitap")]
        public string BookName { get; set; }

        [Display(Name = "Üye Id")]
        public int MemberId { get; set; }

        [Display(Name = "Teslim Durumu")]
        public int TeslimDurumu { get; set; } //0 => teslim edilmedi, 1 => teslim edildi

        [Display(Name = "Teslim Tarihi")]
        public string TeslimTarihi { get; set; }

    }
}