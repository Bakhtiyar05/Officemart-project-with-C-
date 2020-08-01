using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Extensions
{
    public static class IFormFileExtensions
    {


        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpg" ||
                    file.ContentType == "image/jpeg" ||
                    file.ContentType == "image/png" ||
                    file.ContentType == "image/gif";
        }

        public static bool ByteArrayIsImage(this string file)
        {
            return file.Contains("image/jpg") ||
                    file.Contains("image/jpeg") ||
                    file.Contains("image/png") ||
                    file.Contains("image/gif");
        }

        public static async Task<string> SaveImage(this IFormFile image, string root, string subfolder)
        {
            if (!Directory.Exists(Path.Combine(root, "img", subfolder)))
            {
                Directory.CreateDirectory(Path.Combine(root, "img", subfolder));
            }
            string filename = Path.Combine(subfolder, Guid.NewGuid().ToString() + Path.GetFileName(image.FileName));

            string path = Path.Combine(root, "img", filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return filename;
        }

        public static async Task<string> ByteArraySaveImage(this byte[] image, string root, string subfolder)
        {
            if (!Directory.Exists(Path.Combine(root, "img", subfolder)))
            {
                Directory.CreateDirectory(Path.Combine(root, "img", subfolder));
            }
            string filename = Path.Combine(subfolder, Guid.NewGuid().ToString() + DateTime.Now.ToString("MM-yyyy-dd")+".jpg");

            string path = Path.Combine(root, "img", filename);
            await File.WriteAllBytesAsync(path, image);
            return filename;
        }

        public static void RemoveImage(string root, string image)
        {
            string path = Path.Combine(root, "img", image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}
