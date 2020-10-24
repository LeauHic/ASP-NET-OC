using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChoixResto.Models;
using ChoixResto.ViewModels;

namespace ChoixResto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Resto> listeDesRestos = new List<Resto>
                {
                    new Resto { Id = 1, Nom = "Resto pinambour", Telephone = "1234" },
                    new Resto { Id = 2, Nom = "Resto tologie", Telephone = "1234" },
                    new Resto { Id = 5, Nom = "Resto ride", Telephone = "5678" },
                    new Resto { Id = 9, Nom = "Resto toro", Telephone = "555" },
                };
            ViewBag.ListeDesRestos = new SelectList(listeDesRestos, "Id", "Nom");
            return View();
        }
    }
}