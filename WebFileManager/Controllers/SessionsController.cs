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
            
            if (HttpContext.Session.Keys.Contains(name))
            {
                var a = HttpContext.Session.GetString(name);
                return Content(HttpContext.Session.GetString(name));
            }
            else
            {
                return Content(null);
            }
        }
        [HttpPost]
        public void SetSessionValue(string name, string value)
        {
            HttpContext.Session.SetString(name, value);
        }

    }
}
