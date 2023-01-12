using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebAPI_Kutuphane.Models
{
    public class EmployeeModel
    {

        [Required(ErrorMessage = "Adınızı giriniz.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyadınızı giriniz.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "T.C Kimlik numarası giriniz.")]
        [Range(10000000000, 99999999999, ErrorMessage = "Geçerli T.C. kimlik numarası giriniz.")]
        [Display(Name = "T.C. Kimlik No")]
        public Int64 TcNo { get; set; }

        [Display(Name = "E-posta")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "E-posta adresinizi giriniz.")]
        public string Eposta { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Giriniz")]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "Şifre Geçersiz")]
        public string Parola { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Parolanızı onaylayın")]
        [Compare("Parola", ErrorMessage = "Parolanız uyuşmuyor")]
        public string ConfirmParola { get; set; }

    }
}