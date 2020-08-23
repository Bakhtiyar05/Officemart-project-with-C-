using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LangController : Controller
    {
        IWebHostEnvironment _env;
        public LangController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            var a = _env.ContentRootPath;
            return View();
        }
        [HttpPost]
        public IActionResult Index(string key, string value)
        {
            //using (System.Resources.ResXResourceReader rw = new System.Resources.ResXResourceReader(System.IO.Path.Combine(_env.ContentRootPath, "Resources", "SharedResources.ru-RU.resx")))
            //{
            //    foreach (var d in rw)
            //    {
            //        Console.WriteLine(d.Key.ToString() + ":\t" + d.Value.ToString());
            //    }
            //}
            return NoContent();
        }
    }
}
