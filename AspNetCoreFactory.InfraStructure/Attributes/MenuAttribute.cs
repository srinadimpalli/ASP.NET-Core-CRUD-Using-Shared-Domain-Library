using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFactory.InfraStructure.Attributes
{
    public class MenuAttribute : ActionFilterAttribute
    {
        private string _menu { get; set; }
        public MenuAttribute(string menu)
        {
            _menu = menu;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            (context.Controller as Controller).ViewBag.Menu = _menu;
            base.OnActionExecuting(context);
        }
    }
}
