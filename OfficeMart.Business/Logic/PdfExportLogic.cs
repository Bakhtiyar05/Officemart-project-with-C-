using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class PdfExportLogic
    {
        public async Task<FileStreamResult> ExportToPdf(int orderNumberId)
        {
            using (var context = TransactionConfig.AppDbContext)
            {
                var orderNumber = await context
                    .OrderNumbers
                    .FindAsync(orderNumberId);

                var buyedUser = await context.Orders.Where(m => m.OrderNumberId == orderNumberId).FirstOrDefaultAsync();

                var orderNumberProducts = await context
                    .OrderNumbers
                    .Where(x => x.Id == orderNumberId)
                    .Include(x => x.Orders)
                    .ThenInclude(x => x.Product)
                    .ToListAsync();

                PdfDocument document = new PdfDocument();

                PdfPage page = document.Pages.Add();

                PdfGraphics graphics = page.Graphics;

                PdfFont font = new PdfStandardFont(PdfFontFamily.Courier, 15);
                int lineHeight = 40;
                decimal totalSum = 0;
                graphics.DrawString($"Tarix : {DateTime.Now.ToString("dd.MM.yyyy")}", font, PdfBrushes.Black, new PointF(0, lineHeight+=25));
                graphics.DrawString($"Cek № : {orderNumber.OrderCheckNumber}", font, PdfBrushes.Black, new PointF(0, lineHeight+=25));
                graphics.DrawString($"Musteri : {buyedUser.BuyerName} {buyedUser.BuyerSurname}", font, PdfBrushes.Black, new PointF(0, lineHeight+=25));
                graphics.DrawString($"Elaqe : {buyedUser.BuyerPhone}", font, PdfBrushes.Black, new PointF(0, lineHeight+=25));
                graphics.DrawString($"Unvan : {buyedUser.DeliveryAddress}", font, PdfBrushes.Black, new PointF(0, lineHeight+=25));
                PdfGrid pdfGrid = new PdfGrid();
                List<object> data = new List<object>();


                foreach (var order in orderNumber.Orders)
                {

                    var productEntity = orderNumberProducts
                        .Select(x => x.Orders.Select(x => x.Product).Where(x => x.Id == order.Product.Id).FirstOrDefault())
                        .FirstOrDefault();
                    totalSum += productEntity.Count * productEntity.Price;
                    Object row = new { Ad = " " + productEntity.ProductName.ToString(), Say = " " + productEntity.Count.ToString(), Qiymet = " " + productEntity.Price.ToString(), Toplam = " " + (productEntity.Count * productEntity.Price).ToString() };
                    data.Add(row);

                }
                Object total = new { Ad = " ", Say = " ", Qiymet = " ", Toplam = $" {totalSum}" };
                data.Add(total);
                IEnumerable<object> dataTable = data;
                pdfGrid.DataSource = dataTable;

                pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, lineHeight+=25));

                MemoryStream stream = new MemoryStream();

                document.Save(stream);


                stream.Position = 0;

                FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

                fileStreamResult.FileDownloadName = "Sample.pdf";

                return fileStreamResult;


            }
        }



    }
}
