using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCKutuphane.Models
{
    public class MemberModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Üye ismi giriniz.")]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }

        [Required(ErrorMessage = "Telefon numarası giriniz.")]
        [Range(1000000000, 9999999999, ErrorMessage = "Başında 0 olmadan 10 hane olarak tuşlayınız.")]
        [Display(Name = "Telefon Numarası")]
        public Int64 Telefon { get; set; }

        [Display(Name = "E-posta")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "E-posta adresinizi giriniz.")]
        public string Eposta { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Giriniz")]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "Şifre Geçersiz")]
        public string Parola { get; set; }
    }
}