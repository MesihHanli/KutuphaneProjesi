using MVC_WebAPI_Kutuphane.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System.IO;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;

namespace MVCKutuphane.Controllers
{
    public class SignEmployeeController : Controller
    {
        // GET: SignEmployee
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:44383/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                model.Parola = encryptParola(model.Parola);

                HttpResponseMessage response = await client.PostAsJsonAsync("api/SignUpEmployee", model);
                //HttpResponseMessage response = await client.GetAsync("api/SignUpEmployee");
                if (response.IsSuccessStatusCode)
                {
                    int mvalue = await response.Content.ReadAsAsync<int>();
                    client.Dispose(); // client kapat
                    if (mvalue != 0)
                    {
                        FormsAuthentication.SetAuthCookie(model.TcNo.ToString(), false);
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        TempData["Message"] = "E-posta ya da T.C. kimlik numarası daha önce kullanılmış";
                    }
                }
                /*
                if(CreateEmployee(model.Ad, model.Soyad, model.TcNo, model.Eposta, model.Parola) != 0)
                {
                    return RedirectToAction("Index");
                }
                */
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(EmployeeModel model)
        {
            if (ModelState.IsValidField("TcNo"))
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:44383/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                model.Parola = encryptParola(model.Parola);

                HttpResponseMessage response = await client.PostAsJsonAsync("api/SignInEmployee", model);
                if (response.IsSuccessStatusCode)
                {
                    bool mvalue = await response.Content.ReadAsAsync<bool>();
                    client.Dispose(); // client kapat
                    if (mvalue == true)
                    {
                        //HttpCookie cookieVisitor = new HttpCookie("eposta", model.TcNo.ToString());
                        //cookieVisitor.Expires = DateTime.Now.AddDays(1);
                        //Response.Cookies.Add(cookieVisitor);

                        FormsAuthentication.SetAuthCookie(model.TcNo.ToString(), false);
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        TempData["Invalid User"] = "T.C. kimlik numarası ya da şifre hatalı.";
                    }
                }
            }
            return View();
        }

        private string encryptParola(string parola)
        {
            // parola encryption
            var bytesParola = Encoding.Unicode.GetBytes(parola);
            RsaKeyParameters publicKey;
            using (var reader = System.IO.File.OpenText("D:/Projects/MVCKutuphane/Media/public-key.pem"))
                publicKey = (RsaKeyParameters)new PemReader(reader).ReadObject();
            var encryptEngine = new Pkcs1Encoding(new RsaEngine());
            encryptEngine.Init(true, publicKey);
            return Convert.ToBase64String(encryptEngine.ProcessBlock(bytesParola, 0, bytesParola.Length));
        }

    }
}