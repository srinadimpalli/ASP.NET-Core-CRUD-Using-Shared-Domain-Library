using AspNetCoreFactory.InfraStructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Mvc.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        [Menu("Welcome")]
        public IActionResult Welcome() => View();

        [HttpGet("architecture")]
        [Menu("Architecture")]
        public ActionResult Architecture() => View();

        [HttpGet("designexcellence")]
        [Menu("DesignExcellence")]
        public ActionResult DesignExcellence() => View();
    }
}
