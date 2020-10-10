using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Extensions
{
    public static class IFormFileExtensions
    {
        public static byte[] OptimizeImageSize(this IFormFile file, int maxWidth, int maxHeight)
        {
            using (var stream = file.OpenReadStream())
            using (var image = Image.Load(stream))
            {
                using (var writeStream = new MemoryStream())
                {
                    //if (image.Width > maxWidth)
                    //{
                    //    var thumbScaleFactor = maxWidth / image.Width;
                    //    image.Mutate(i => i.Resize(maxWidth, image.Height *
                    //        thumbScaleFactor));
                    //}
                    if (image.Height > maxHeight)
                    {
                        var thumbScaleFactor = maxHeight / image.Height;
                        image.Mutate(i => i.Resize(maxHeight, image.Width *
                            thumbScaleFactor));
                    }
                    image.SaveAsPng(writeStream);
                    return writeStream.ToArray();
                }
            }
        }
    }
}
