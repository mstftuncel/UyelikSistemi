using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UyelikSistemi.Models;
using System.Data.Entity;
using System.Windows;

namespace UyelikSistemi.Controllers
{
    public class YorumController : Controller
    {
        // GET: Yorum
        [HttpPost]
        public ActionResult Index(string CommentText)
        {
            UyeSistemiEntities1 db = new UyeSistemiEntities1();
 
            uyeYorum yorum = new uyeYorum();
            yorum.yorum = CommentText;

            db.uyeYorum.Add(yorum);
            db.SaveChanges();
            MessageBox.Show("Yorumunuz Kaydedildi. ");


            return RedirectToAction("Olay","Account");
        }
    }
}