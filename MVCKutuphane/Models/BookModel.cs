using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCKutuphane.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap ismi giriniz")]
        [Display(Name = "Kitap İsmi")]
        public string Isim { get; set; }

        [Required(ErrorMessage = "Yazar giriniz")]
        [Display(Name = "Yazar")]
        public string Yazar { get; set; }

        [Required(ErrorMessage = "Kitap türü giriniz")]
        [Display(Name = "Kitap Türü")]
        public string Tur { get; set; }

        [Required(ErrorMessage = "Sayfa sayısı giriniz")]
        [Display(Name = "Sayfa Sayısı")]
        public int Sayfa { get; set; }

        public string Durum { get; set; }

    }
}