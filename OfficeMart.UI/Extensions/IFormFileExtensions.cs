using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OfficeMart.UI
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

        public async static Task<string> SavePhotoAsync(this IFormFile file, string root, string folder, string baseUrl)
        {
            string imgFolder = Path.Combine(root, "img", folder);
            if (!Directory.Exists(imgFolder))
            {
                Directory.CreateDirectory(imgFolder);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string filePath = Path.Combine(imgFolder, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            string imageRelativePath = $"img/{folder}/{fileName}";
            string fullImageUrl = $"{baseUrl}{imageRelativePath}";

            return fullImageUrl;
        }

        public static bool RemovePhoto(string root, string relativePath)
        {
            string fullPath = Path.Combine(root, relativePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

    }
}
