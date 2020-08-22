using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    public class ExportController : Controller
    {
        public async Task<IActionResult> ExcelExport(string startDate, string endDate)
        {
            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);
            var products = await new ExportLogic().GetDatasForExport(start, end);
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName =DateTime.Now.ToString("dd-MM-yyyy")+"hesaba.xlsx";
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Hesabat");

                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Ad";
                    worksheet.Cell(1, 3).Value = "Mövcud Qiymət";
                    worksheet.Cell(1, 4).Value = "Satış Qiyməti";
                    worksheet.Cell(1, 5).Value = "Cəmi";
                    worksheet.Cell(1, 6).Value = "Mövcud Sayı";
                    worksheet.Cell(1, 7).Value = "Satış Sayı";
                    worksheet.Cell(1, 8).Value = "Satış Tarixi";
                    worksheet.Cell(1, 9).Value = "Müştəri Adı";
                    worksheet.Cell(1, 10).Value = "Çatdırılma Adresi";
                    worksheet.Cell(1, 11).Value = "Əlaqə Nömrəsi";

                    for (int index = 1; index <= products.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value =
                        products[index - 1].ProductId;
                        worksheet.Cell(index + 1, 2).Value =
                        products[index - 1].BuyerName + " " + products[index - 1].BuyerSurname;
                        worksheet.Cell(index + 1, 3).Value =
                        products[index - 1].Product.Price;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
