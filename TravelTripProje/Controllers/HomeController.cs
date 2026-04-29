using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTripProje.Models.Siniflar;

namespace TravelTripProje.Controllers
{
    public class HomeController : Controller
    {
        Context c = new Context();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View(new Iletisim());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Iletisim model)
        {
            if (model == null ||
                string.IsNullOrWhiteSpace(model.AdSoyad) ||
                string.IsNullOrWhiteSpace(model.Mail) ||
                string.IsNullOrWhiteSpace(model.Konu) ||
                string.IsNullOrWhiteSpace(model.Mesaj))
            {
                TempData["MesajTipi"] = "hata";
                TempData["Mesaj"] = "Lütfen tüm alanları doldurun.";
                return View(model ?? new Iletisim());
            }

            c.Iletisims.Add(model);
            c.SaveChanges();

            TempData["MesajTipi"] = "ok";
            TempData["Mesaj"] = "Mesajınız alındı, teşekkürler.";
            return RedirectToAction("Contact");
        }
    }
}