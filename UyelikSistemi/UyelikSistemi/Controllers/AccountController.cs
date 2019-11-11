using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Windows;
using UyelikSistemi.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.Entity;

namespace UyelikSistemi.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
       
        public ActionResult Index()
        {
  
            return View();

        }
        private UyeSistemiEntities1 db = new UyeSistemiEntities1();
        public ActionResult Olay()
        {
            string[] yasaklı = { "pis_kelime1", "kötü_kelime1", "yasak_kelime1" };
            
            System.Collections.ArrayList koleksiyon = new System.Collections.ArrayList();
            koleksiyon.AddRange(yasaklı);

            var comments = db.uyeYorum.Include(x => x.yorum).ToList();
            return View(comments);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            //Random sifre olusturma
            int sayi = 8;
            string karakter = "abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ0123456789";
            StringBuilder sifre = new StringBuilder();
            Random rdm = new Random();
            while (sayi-- > 0)
                sifre.Append(karakter[rdm.Next(karakter.Length)]);
            string a = sifre.ToString();
            //DB ekleme
            UyeSistemiEntities1 db = new UyeSistemiEntities1();
            uyeKayit model = new uyeKayit();
            model.ad = form["Username"].Trim();
            model.soyad = form["Surname"].Trim();
            model.sifre = a;
            db.uyeKayit.Add(model);
            //Sifreleme
            string anametin = a;
            byte[] sifreli = ASCIIEncoding.ASCII.GetBytes(anametin);
            string sifrelimetin = Convert.ToBase64String(sifreli);
            model.sifre = sifrelimetin;
            db.SaveChanges();
            MessageBox.Show("Kayıt Başarılı. " +
            " Kullanıcı adınız: " + model.kullaniciadi +
            " Şifreniz: " + a);

            return RedirectToAction("Index","Account");
           
        }
    }
}