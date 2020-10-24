using ChoixResto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class AjouterRestaurantController : Controller
    {
        // GET: AjouterRestaurant
        public ActionResult Index()
        {
            return View("AjouterRestaurant");
        }
        [HttpPost]
        public ActionResult AjouterRestaurant(Resto resto)
        {
            using(IDal dal = new Dal())
            {
                if (dal.RestaurantExiste(resto.Nom))
                {
                    ModelState.AddModelError("Nom", "Le restaurant existe déjà");
                    return View(resto);
                }
                if (!ModelState.IsValid)
                    return View(resto);
                dal.CreerRestaurant(resto.Nom, resto.Telephone);
                return RedirectToAction("Index");
            }
        }
    }
}