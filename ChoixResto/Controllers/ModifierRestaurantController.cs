﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class ModifierRestaurantController : Controller
    {
        // GET: ModifierRestaurant
        public ActionResult Index(string id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}