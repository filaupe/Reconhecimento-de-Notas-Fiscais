﻿using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;

namespace PrototipoLeitorTesseract.Services
{
    public class ImageService
    {
        public async Task<byte[]> IFormFileReader(IFormFile image)
        {
            using var stream = new MemoryStream();
            await image.CopyToAsync(stream);
            return stream.ToArray();
        }

        public async Task<Image<Bgr, byte>?> ImageResizer(IFormFile image)
        {
            var imageBytes = await this.IFormFileReader(image);

            var imagemOriginal = new Mat();
            CvInvoke.Imdecode(imageBytes, ImreadModes.AnyColor, imagemOriginal);

            int novaLargura = imagemOriginal.Width * 2;
            int novaAltura = imagemOriginal.Height * 2;

            var imagemRedimensionada = new Mat();

            CvInvoke.Resize(imagemOriginal, imagemRedimensionada, new Size(novaLargura, novaAltura), interpolation: Inter.Linear);

            return imagemRedimensionada.ToImage<Bgr, byte>();
        }

        public async Task<string> ImageSaver(IFormFile image, string path = @"wwwroot\images\temp_image.png")
        {
            using var rezedImage = await this.ImageResizer(image);
            rezedImage?.Save(path);
            return path;
        }

        public void DeleteImage(string path = @"wwwroot\images\temp_image.png")
        {
            File.Delete(path);
        }
    }
}
