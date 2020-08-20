using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Controllers
{
    public class AjaxController : ControllerBase
    {
        public async Task<IActionResult> GetByIdForQuickView(int id)
        {
            var product = await new AjaxLogic().GetProductForQuickView(id);
     
           return Ok(product);
        }
    }
}
