using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Movie.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Welcome(string name, string id)
        {
            return $"Hello, Your name's {name}, your id's {id}";
        }
    }
}
