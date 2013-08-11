#region License
/* TrueTypeSharp
   Copyright (c) 2010 Illusory Studios LLC

   TrueTypeSharp is available at zer7.com. It is a C# port of Sean Barrett's
   C library stb_truetype, which was placed in the public domain and is
   available at nothings.org.

   Permission to use, copy, modify, and/or distribute this software for any
   purpose with or without fee is hereby granted, provided that the above
   copyright notice and this permission notice appear in all copies.

   THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
   WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
   MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
   ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
   WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
   ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
   OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
*/
#endregion

using System;
using System.Drawing;
using System.IO;

namespace TrueTypeSharp.Demo
{
    class Program
    {
        static void SaveBitmap(byte[] data, int x0, int y0, int x1, int y1,
            int stride, string filename)
        {
            var bitmap = new Bitmap(x1 - x0, y1 - y0);
            for (int y = y0; y < y1; y++)
            {
                for (int x = x0; x < x1; x++)
                {
                    byte opacity = data[y * stride + x];
                    bitmap.SetPixel(x - x0, y - y0, Color.FromArgb(opacity, 0x00, 0x7f, 0xff));
                }
            }
            bitmap.Save(filename);
        }

        static void Main(string[] args)
        {
            var font = new TrueTypeFont(new FileStream(@"Anonymous/Anonymous Pro.ttf", FileMode.Open));

            // Render some characters...
            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                int width, height, xOffset, yOffset;
                float scale = font.GetScaleForPixelHeight(80);
                byte[] data = font.GetCodepointBitmap(ch, scale, scale,
                    out width, out height, out xOffset, out yOffset);

                SaveBitmap(data, 0, 0, width, height, width, "Char-" + ch.ToString() + ".png");
            }

            // Let's try baking. Tasty tasty.
            BakedCharCollection characters; float pixelHeight = 18;
            var bitmap = font.BakeFontBitmap(pixelHeight, out characters, true);

            SaveBitmap(bitmap.Buffer, 0, 0, bitmap.Width, bitmap.Height, bitmap.Width, "BakeResult1.png");

			// TODO: implement serialization equivalent that is compatible with a Portable Class Library ...
        }
    }
}
