using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
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

                var buyedUser = await context.Users.Where(m => m.Id == orderNumber.BuyerUserId).FirstOrDefaultAsync();

                var orderNumberProducts = await context
                    .OrderNumbers
                    .Where(x => x.Id == orderNumberId)
                    .Include(x => x.Orders)
                    .ThenInclude(x => x.Product)
                    .ToListAsync();






                foreach (var item in orderNumberProducts)
                {
                    foreach (var order in item.Orders)
                    {
                       
                        var productEntity = orderNumberProducts
                            .Select(x => x.Orders.Select(x => x.Product).Where(x => x.Id == order.Product.Id).FirstOrDefault())
                            .FirstOrDefault();
                       

                    }
                }




                PdfDocument document = new PdfDocument();

                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for the page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Draw the text
                graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

                //Saving the PDF to the MemoryStream
                MemoryStream stream = new MemoryStream();

                document.Save(stream);

                //Set the position as '0'.
                stream.Position = 0;

                //Download the PDF document in the browser
                FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

                fileStreamResult.FileDownloadName = "Sample.pdf";

                return fileStreamResult;


            }
        }



    }
}
