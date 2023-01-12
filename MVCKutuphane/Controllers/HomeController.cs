using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MVCKutuphane.Models;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Encodings;
using MVC_WebAPI_Kutuphane.Models;// :/

namespace MVCKutuphane.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/LateBook");

            if (response.IsSuccessStatusCode)
            {
                List<BookMemberModel> bookmemberlist = response.Content.ReadAsAsync<List<BookMemberModel>>().Result;
                return View(bookmemberlist);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> UyariGonder(int MemberId, string BookName,string ActionName) // index
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Notification", new NotificationModel { MemberId = MemberId, Bildirim = $"{BookName} isimli kitabın son teslim tarihi geçmiştir. En kısa zamanda teslim ediniz!" });

            if (!response.IsSuccessStatusCode)
            {
                //Hata Mesajı
            }
            bool a = response.Content.ReadAsAsync<bool>().Result;
            if (a == true) TempData["Bildirim"] = "Bildirim Gönderildi";
            else TempData["Bildirim"] = "Bildirim Gönderilemedi!";
            return RedirectToAction(ActionName);
        }

        public async Task<ActionResult> TeslimiYakin()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Deadline");

            if (response.IsSuccessStatusCode)
            {
                List<BookMemberModel> bookmemberlist = response.Content.ReadAsAsync<List<BookMemberModel>>().Result;
                return View(bookmemberlist);
            }
            else
            {
                return View();
            }
        }

        public ActionResult KitapSorgula()
        {
            return View("KitapSorgula");
        }
        [HttpPost]
        public async Task<ActionResult> KitapSorgula(string searchPhrase)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Book?searchPhrase={searchPhrase}");

            if (response.IsSuccessStatusCode)
            {
                List<BookModel> books = response.Content.ReadAsAsync<List<BookModel>>().Result;
                return View("KitapSorgula", books);
            } 
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> TeslimAl(int TeslimId, string ActionName) // kitapsorgula sayfası
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Borrow/{TeslimId}", new BookMemberModel { TeslimDurumu = 1 });

            if (!response.IsSuccessStatusCode)
            {
                //Hata Mesajı
            }
            bool a = response.Content.ReadAsAsync<bool>().Result;
            return RedirectToAction(ActionName);
        }

        [HttpGet]
        public async Task<ActionResult> Borrow(int bookid)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/SignInMember");
            if (response.IsSuccessStatusCode)
            {
                List<MemberModel> members = response.Content.ReadAsAsync<List<MemberModel>>().Result;
                ViewBag.MemberList = MemberModelListToSelectList(members); ;
            }
            else
            {
                //Hata Mesajı
            }

            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Borrow(int bookid, BookMemberModel model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            model.BookId = bookid;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Borrow", model);

            if (response.IsSuccessStatusCode)
            {
                bool isSuccess = response.Content.ReadAsAsync<bool>().Result;
                if (isSuccess)
                {
                    return RedirectToAction("KitapSorgula");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> Details(int BookId)//
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/BookMember?bookId={BookId}");

            if (response.IsSuccessStatusCode)
            {
                List<MemberModel> memberlist = response.Content.ReadAsAsync<List<MemberModel>>().Result;
                return View(memberlist);
            }
            else
            {
                return View();
            }
        }
        public async Task<ActionResult> DeleteBook(int BookId)//
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.DeleteAsync($"api/Book/{BookId}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Silindi"] = "Kitap Çıkarıldı!";
                return RedirectToAction("KitapSorgula");
            }
            else
            {
                return RedirectToAction("KitapSorgula");
            }
        }


        public ActionResult UyeIslem()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UyeIslem(MemberModel model)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:44383/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                model.Parola = encryptParola(model.Parola);

                HttpResponseMessage response = await client.PostAsJsonAsync("api/SignUpMember", model);
                if (response.IsSuccessStatusCode)
                {
                    int mvalue = await response.Content.ReadAsAsync<int>();
                    client.Dispose();
                    if (mvalue != 0)
                    {
                        TempData["memberKayitBasarili"] = "Üye Başarıyla Kaydedildi";
                        TempData["memberKayitBasarisiz"] = "";

                    }
                    else
                    {
                        TempData["memberKayitBasarisiz"] = "E-posta ya da telefon numarası daha önce kullanılmış";
                        TempData["memberKayitBasarili"] = "";

                    }
                }
            }

            return View();
        }
        public ActionResult UyeSorgula()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UyeSorgula(string searchPhrase)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/SignInMember?searchPhrase={searchPhrase}");

            if (response.IsSuccessStatusCode)
            {
                List<MemberModel> members = response.Content.ReadAsAsync<List<MemberModel>>().Result;
                return View("UyeSorgula", members);
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public async Task<ActionResult> KitapKayit()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/BookType");
            if (response.IsSuccessStatusCode)
            {
                List<BookTypeModel> bookTypes = response.Content.ReadAsAsync<List<BookTypeModel>>().Result;
                ViewBag.BookTypeList = BookTypeModelListToSelectList(bookTypes); ;
            }
            else
            {
                //Hata Mesajı
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> KitapKayit(BookModel model)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:44383/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Book", model);
                if (response.IsSuccessStatusCode)
                {
                    int mvalue = await response.Content.ReadAsAsync<int>();
                    client.Dispose();
                    if (mvalue != 0)
                    {
                        TempData["bookKayitBasarili"] = "Kitap Başarıyla Kaydedildi";
                        TempData["bookKayitBasarisiz"] = "";

                    }
                    else
                    {
                        TempData["bookKayitBasarili"] = "Hata!";
                    }
                }
            }

            return RedirectToAction("KitapKayit");
        }

        [HttpGet]
        public async Task<ActionResult> KitapTuruDuzenle()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/BookType");
            if (response.IsSuccessStatusCode)
            {
                List<BookTypeModel> bookTypes = response.Content.ReadAsAsync<List<BookTypeModel>>().Result;
                return View("KitapTuruDuzenle", bookTypes);
            }
            else
            {
                //Hata Mesajı
            }

            return View();
        }

        [HttpGet]
        public ActionResult KitapTuruEkle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> KitapTuruEkle(BookTypeModel model)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:44383/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/BookType", model);
                if (response.IsSuccessStatusCode)
                {
                    bool mvalue = await response.Content.ReadAsAsync<bool>();
                    client.Dispose();
                    if (mvalue)
                    {
                        TempData["BookTypeKayit"] = "Kitap Türü Başarıyla Kaydedildi";

                    }
                    else
                    {
                        TempData["BookTypeKayit"] = "Hata!";
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> KitapTuruSil(int BookTypeId)//
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.DeleteAsync($"api/BookType/{BookTypeId}");

            if (response.IsSuccessStatusCode)
            {
                TempData["KitapTuruSilme"] = "Kitap Türü Silindi!";
                return RedirectToAction("KitapTuruDuzenle");
            }
            else
            {
                return RedirectToAction("KitapTuruDuzenle");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Profil(string tc)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Employee?tc={tc}");
            if (response.IsSuccessStatusCode)
            {
                List<EmployeeModel> employees = response.Content.ReadAsAsync<List<EmployeeModel>>().Result;
                return View(employees[0]);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult AdSoyadDuzenle()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AdSoyadDuzenle(EmployeeModel model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/EmployeeName",model);
            if (response.IsSuccessStatusCode)
            {
                bool mvalue = await response.Content.ReadAsAsync<bool>();
                if(mvalue == true)
                {
                    TempData["AdSoyadDuzenle"] = "Ad ve Soyad Güncellendi";
                }
                else
                {
                    TempData["AdSoyadDuzenle"] = "Hata!";
                }
            }
            else
            {
                    TempData["AdSoyadDuzenle"] = "Hata!";
            }
            return RedirectToAction("Profil",new { tc = model.TcNo });
        }

        [HttpGet]
        public ActionResult EpostaDuzenle()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> EpostaDuzenle(EmployeeModel model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/EmployeeEmail", model);
            if (response.IsSuccessStatusCode)
            {
                bool mvalue = await response.Content.ReadAsAsync<bool>();
                if (mvalue == true)
                {
                    TempData["EpostaDuzenle"] = "Eposta Güncellendi";
                }
                else
                {
                    TempData["EpostaDuzenle"] = "Hata!";
                }
            }
            else
            {
                TempData["EpostaDuzenle"] = "Hata!";
            }
            return RedirectToAction("Profil", new { tc = model.TcNo });
        }
        [HttpGet]
        public ActionResult ParolaDuzenle()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ParolaDuzenle(EmployeeModel model)
        {
            model.Parola = encryptParola(model.Parola);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44383/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/EmployeePassword", model);//
            if (response.IsSuccessStatusCode)
            {
                bool mvalue = await response.Content.ReadAsAsync<bool>();
                if (mvalue == true)
                {
                    TempData["ParolaDuzenle"] = "Şifre Güncellendi";
                }
                else
                {
                    TempData["ParolaDuzenle"] = "Hata!";
                }
            }
            else
            {
                TempData["ParolaDuzenle"] = "Hata!";
            }
            return RedirectToAction("Profil", new { tc = model.TcNo });
        }
        [HttpGet]
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn", "SignEmployee");
        }




        //Non Actions
        private string encryptParola(string parola)
        {
            // parola encryption
            var bytesParola = Encoding.Unicode.GetBytes(parola);
            RsaKeyParameters publicKey;
            using (var reader = System.IO.File.OpenText(@"D:/Projects/MVCKutuphane/Media/public-key.pem"))
                publicKey = (RsaKeyParameters)new PemReader(reader).ReadObject();
            var encryptEngine = new Pkcs1Encoding(new RsaEngine());
            encryptEngine.Init(true, publicKey);
            return Convert.ToBase64String(encryptEngine.ProcessBlock(bytesParola, 0, bytesParola.Length));
        }

        [NonAction]
        public SelectList BookTypeModelListToSelectList(List<BookTypeModel> bookTypes)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (BookTypeModel row in bookTypes)
            {
                list.Add(new SelectListItem()
                {
                    Text = row.Tur.ToString(),
                    Value = row.Tur.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }
        [NonAction]
        public SelectList MemberModelListToSelectList(List<MemberModel> members)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (MemberModel row in members)
            {
                list.Add(new SelectListItem()
                {
                    Text = row.AdSoyad + " - " + row.Id.ToString(),
                    Value = row.Id.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

    }
}