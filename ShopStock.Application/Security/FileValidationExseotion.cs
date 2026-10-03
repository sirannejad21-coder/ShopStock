using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Security
{
    public static class FileValidationExseotion
    {

        public static bool ImageValid(this IFormFile image)
        {

            string[] allowExeption = { ".jpg", ".jpeg", ".gif", ".webp" };

            var fileExecption=Path.GetExtension(image.FileName).ToLower();

            if (!allowExeption.Contains(fileExecption)) { return false; }

            return true;

        }

    }
}
