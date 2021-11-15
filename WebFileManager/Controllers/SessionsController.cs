using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebFileManager.Controllers
{
    public class SessionsController : Controller
    {

        [HttpPost]
        public ContentResult GetSessionValueByName(string name)
        {
            var a = HttpContext.Session;
            if (HttpContext.Session.Keys.Contains(name))
            {
                return Content(HttpContext.Session.GetString(name), "application/json");
            }
            else
            {
                return Content(null, "application/json");
            }
        }
        [HttpPost]
        public void SetSessionValue(string name, string value)
        {
            HttpContext.Session.SetString(name, value);
        }

    }
}
