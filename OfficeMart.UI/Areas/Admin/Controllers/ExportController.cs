using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ExportController : Controller
    {
        public async Task<IActionResult> ExcelExport(DateTime beginDate, DateTime endDate)
        {

            var products = await new ExportLogic().GetDatasForExport(beginDate, endDate);
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = beginDate.ToString("dd-MM-yyyy")+"-"+ endDate.ToString("dd-MM-yyyy") + "Hesabat.xlsx";
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Hesabat");

                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Kateqoriya";
                    worksheet.Cell(1, 3).Value = "Məsul Adı";
                    worksheet.Cell(1, 4).Value = "Mövcud Qiyməti (manat)";
                    worksheet.Cell(1, 5).Value = "Mövcud Sayı (ədəd)";
                    worksheet.Cell(1, 6).Value = "Satış Sayı (ədəd)";
                    worksheet.Cell(1, 7).Value = "Satış Qiyməti (ədədə göra manat)";
                    worksheet.Cell(1, 8).Value = "Cəmi Qiyməti (manat)";
                    worksheet.Cell(1, 9).Value = "Satış Tarixi";
                    worksheet.Cell(1, 10).Value = "Müştəri Adı";
                    worksheet.Cell(1, 11).Value = "Çatdırılma Adresi";
                    worksheet.Cell(1, 12).Value = "Əlaqə Nömrəsi";

                    for (int index = 1; index <= products.Count; index++)
                    {
                        OrderDto currentSale = products[index - 1];
                        int row = index + 1;
                        worksheet.Cell(row, 1).Value = currentSale.ProductId;
                        worksheet.Cell(row, 2).Value = currentSale.Product.Category.CategoryName;
                        worksheet.Cell(row, 3).Value = currentSale.Product.ProductName;
                        worksheet.Cell(row, 4).Value = currentSale.Product.Price;
                        worksheet.Cell(row, 5).Value = currentSale.Product.Count;
                        if (currentSale.Product.Count < 1) worksheet.Cell(row, 5).Style.Fill.BackgroundColor = XLColor.BrickRed;
                        worksheet.Cell(row, 6).Value = currentSale.OrderCount;
                        worksheet.Cell(row, 7).Value = currentSale.SaledPrice;
                        worksheet.Cell(row, 8).Value = currentSale.TotalPrice;
                        worksheet.Cell(row, 9).Value = currentSale.RegDate;
                        worksheet.Cell(row, 10).Value = currentSale.BuyerName + " " + currentSale.BuyerSurname;
                        worksheet.Cell(row, 11).Value = currentSale.DeliveryAddress;
                        worksheet.Cell(row, 12).Value = currentSale.BuyerPhone;
                        if (index == products.Count)
                        {
                            worksheet.Cell(row + 1, 8).Value =$"Cəmi Summa {products.Sum(m => m.TotalPrice)}";
                            worksheet.Cell(row + 1, 8).Style.Fill.BackgroundColor = XLColor.Aqua;
                        
                        }
                    }
                    worksheet.Columns().AdjustToContents();
                    
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
