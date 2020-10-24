using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChoixResto.Models;

namespace ChoixResto.Controllers
{

    public class RestaurantController : Controller
    {
        
        public BddContext bdd = new BddContext();
        // GET: Restaurant
        public ActionResult Index()
        {
            using(IDal dal = new Dal())
            {
                List<Resto> listResto = dal.ObtientTouslesRestaurants();
                return View(listResto);
            }
        }

        public ActionResult ModifierRestaurant(int? id)
        {
            if (id.HasValue && id <= bdd.Restos.Count())
            {
                using (IDal dal = new Dal())
                {
                    Resto restoRecherche = bdd.Restos.FirstOrDefault(Resto => Resto.Id == id);
                    return View(restoRecherche);
                }
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ModifierRestaurant(Resto resto)
        {
            if (!ModelState.IsValid)
            {
                return View(resto);
            }
            using (IDal dal = new Dal())
            {
                dal.ModifierRestaurant(resto.Id, resto.Nom, resto.Telephone);
                return RedirectToAction("Index");
            }
        }
    }
}