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
using ICSharpCode.Decompiler.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;

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
        public async Task<IActionResult> Index(string key, string value)
        {
            var result = await new LangLogic().AddResource(key, value);
            ViewBag.IsSuccessfullAdded = result.OperationIsSuccessfull;

            var resourcesFile = await new LangLogic().GetAllResources();
            using (ResXResourceWriter resx = new ResXResourceWriter(System.IO.Path.Combine(_env.ContentRootPath, "Resources", "SharedResources.ru-RU.resx")))
            {
                foreach (var resource in resourcesFile)
                {
                    resx.AddResource(resource.Name, resource.Value);
                }
            }
            return View();
        }
    }
}
