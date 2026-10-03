using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Convertor
{
    public  class ImageResizer
    {

        public void ImageResize(string InputPath, string OutPutPath,int? With,int? Height)
        {
            var CustomWith = With ?? 120;
            var CustomHeight = Height ?? 210;

            using (var image = Image.Load(InputPath)) { 
            
            image.Mutate(i=>i.Resize(CustomWith, CustomHeight));

                image.Save(OutPutPath,new JpegEncoder{ 
                
                Quality=100
                
                });
            
            }




        }

    }
}
