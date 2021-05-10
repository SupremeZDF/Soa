using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcOneTest
{
    public class OneTestController : Controller
    {
        public ViewResult actionResult() 
        {
            var dd = typeof(int);
            return View("~/Views/Home/About.cshtml");
        }
    }
}
