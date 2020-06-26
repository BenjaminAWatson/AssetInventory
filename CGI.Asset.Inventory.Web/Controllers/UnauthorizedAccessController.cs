using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CGI.Asset.Inventory.Web.Controllers
{
    public class UnauthorizedAccessController : Controller
    {
        // GET: UnauthorizedAccess
        public ActionResult Index()
        {
            return View();
        }
    }
}