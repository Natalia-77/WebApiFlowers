﻿using System;
using System.Drawing;
using System.IO;

namespace HelperLibrary
{
    public static class ImageConverterBase
    {
        /// <summary>
        /// Перетворення зображення по вказаному шляху в base64.
        /// </summary>
        /// <param name="path_file"></param>
        /// <returns></returns>
        public static string ConvertToBase(string path_file)
        {
            using Image image = Image.FromFile(path_file);
            using MemoryStream m = new MemoryStream();
            image.Save(m, image.RawFormat);
            byte[] imageBytes = m.ToArray();            
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        public static Bitmap Base64ToImage(this string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return new Bitmap(image);
        }
    }
}
