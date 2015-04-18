using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Doorway_Studio.Images
{
    class ImageBuilder
    {
        #region Data
        protected string text;
        protected int width;
        protected int height;
        protected string familyName;

        protected Bitmap image;
        #endregion

        #region Properties
        /// <summary>
        /// Image
        /// </summary>
        public Bitmap Image
        {
            get
            {
                return image;
            }
        }
        // Все свойства можно только читать
        /// <summary>
        /// Text
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }
        }
        /// <summary>
        /// Высота
        /// </summary>
        public int Width
        {
            get
            {
                return this.width;
            }
        }
        /// <summary>
        /// Ширина
        /// </summary>
        public int Height
        {
            get
            {
                return this.height;
            }
        }
        #endregion
    }

    class CaptchaImageOne : ImageBuilder
    {

        /// <summary>
        /// Конструктор, создает картинку
        /// </summary>
        /// <param name="s">Текст</param>
        /// <param name="width">Высота</param>
        /// <param name="height">Ширина</param>
        public CaptchaImageOne(string s, int width, int height)
        {
            this.text = s;
            this.width = width;
            this.height = height;
            this.GenerateImage();
        }

        /// <summary>
        /// Конструктор, создает картинку
        /// </summary>
        /// <param name="s">Текст</param>
        /// <param name="width">Высота</param>
        /// <param name="height">Ширина</param>
        /// <param name="familyName">Шрифт</param>
        public CaptchaImageOne(string s, int width, int height, string familyName)
        {
            this.text = s;
            this.width = width;
            this.height = height;
            this.SetFamilyName(familyName);
            this.GenerateImage();
        }

        // ====================================================================
        // This member overrides Object.Finalize.
        // ====================================================================
        ~CaptchaImageOne()
        {
            Dispose(false);
        }

        // ====================================================================
        // Releases all resources used by this object.
        // ====================================================================
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        // ====================================================================
        // Custom Dispose method to clean up unmanaged resources.
        // ====================================================================
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                // Dispose of the bitmap.
                this.image.Dispose();
        }

        // ====================================================================
        // Sets the font used for the image text.
        // ====================================================================
        private void SetFamilyName(string familyName)
        {
            // If the named font is not installed, default to a system font.
            try
            {
                Font font = new Font(this.familyName, 12F);
                this.familyName = familyName;
                font.Dispose();
            }
            catch (Exception)
            {
                this.familyName = System.Drawing.FontFamily.GenericSerif.Name;
            }
        }

        // ====================================================================
        // Creates the bitmap image.
        // ====================================================================
        private void GenerateImage()
        {
            // For generating random numbers.
            Random random = new Random();
            // Create a new 32-bit bitmap image.
            Bitmap bitmap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);

            // Create a graphics object for drawing.
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.width, this.height);

            // Fill in the background.
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
            g.FillRectangle(hatchBrush, rect);

            // Set up the text font.
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            // Adjust the font size until the text fits within the image.
            do
            {
                fontSize--;
                font = new Font(this.familyName, fontSize, FontStyle.Bold);
                size = g.MeasureString(this.text, font);
            } while (size.Width > rect.Width);

            // Set up the text format.
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Create a path using the text and warp it randomly.
            GraphicsPath path = new GraphicsPath();
            path.AddString(this.text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            float v = 4F;
            PointF[] points =
			{
				new PointF(random.Next(rect.Width) / v, random.Next(rect.Height) / v),
				new PointF(rect.Width - random.Next(rect.Width) / v, random.Next(rect.Height) / v),
				new PointF(random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v),
				new PointF(rect.Width - random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v)
			};
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

            // Draw the text.
            hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, Color.DarkGray);
            g.FillPath(hatchBrush, path);

            // Add some random noise.
            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                int x = random.Next(rect.Width);
                int y = random.Next(rect.Height);
                int w = random.Next(m / 50);
                int h = random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }

            // Clean up.
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            // Set the image.
            this.image = bitmap;
        }
    }

    class CaptchaImageTwo : ImageBuilder
    {
        private Font font;
        private string fontname = "arial black";
        private float fontsize;
        private FontStyle fontstyle = FontStyle.Italic;
        private Brush fontcolor = Brushes.Black;

        private int points;


        #region Properties
        /// <summary>
        /// Имя шрифта
        /// </summary>
        public string FontName
        {
            get
            {
                return fontname;
            }
        }
        /// <summary>
        /// Размер шрифта
        /// </summary>
        public float FontSize
        {
            get
            {
                return fontsize;
            }
        }
        /// <summary>
        /// Стиль шрифта
        /// </summary>
        public FontStyle FontStyle
        {
            get
            {
                return fontstyle;
            }
        }
        /// <summary>
        /// Кисть
        /// </summary>
        public Brush FontColor
        {
            get
            {
                return fontcolor;
            }
        }

        /// <summary>
        /// Количество точек в кривой
        /// </summary>
        public int Points
        {
            get
            {
                return points;
            }
        }
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Text">Текст</param>
        /// <param name="Width">Высота</param>
        /// <param name="Height">Ширина</param>
        public CaptchaImageTwo(string Text, int Width, int Height)
        {
            this.text = Text;
            this.width = Width;
            this.height = Height;
            this.GenerateImage();
        }


        /// <summary>
        /// Генерирование картинки
        /// </summary>
        /// <returns></returns>
        private void GenerateImage()
        {
            Random random = new Random();

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format16bppRgb555);

            this.points = random.Next(5, 15);
            this.fontsize = random.Next(18, 48);

            Rectangle rect = new Rectangle(0, 0, width, height);


            StringFormat sFormat = new StringFormat();
            sFormat.Alignment = StringAlignment.Center;
            sFormat.LineAlignment = StringAlignment.Center;

            Graphics g = Graphics.FromImage(bmp);


            // Set up the text font.
            SizeF size;
            float fontSize = fontsize + 1;
            Font tempfont;

            // try to use requested font, but
            // If the named font is not installed, default to a system font.
            try
            {
                tempfont = new Font(fontname, fontsize);
                tempfont.Dispose();
            }
            catch (Exception)
            {
                fontname = System.Drawing.FontFamily.GenericSerif.Name;
            }


            // build a new string with space through the chars
            // i.e. keyword 'hello' become 'h e l l o '
            string tempKey = "";

            for (int ii = 0; ii < text.Length; ii++)
            {
                tempKey = String.Concat(tempKey, text[ii].ToString());
                tempKey = String.Concat(tempKey, " ");
            }

            // Adjust the font size until the text fits within the image.
            do
            {
                fontSize--;
                tempfont = new Font(fontname, fontSize, fontstyle);
                size = g.MeasureString(tempKey, tempfont);
            } while (size.Width > (0.8 * bmp.Width));

            font = tempfont;


            g.Clear(Color.Silver); // blank the image
            g.SmoothingMode = SmoothingMode.AntiAlias; // antialias objects

            // fill with a liner gradient
            // random colors
            g.FillRectangle(
                new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(bmp.Width, bmp.Height),
                    Color.FromArgb(
                        255, //randomGenerator.Next(255),
                        random.Next(255),
                        random.Next(255),
                        random.Next(255)
                    ),
                    Color.FromArgb(
                        random.Next(100),
                        random.Next(255),
                        random.Next(255),
                        random.Next(255)
                    )),
                rect);


            // Add some random noise.
            HatchBrush hatchBrush = new HatchBrush(
                HatchStyle.LargeConfetti,
                Color.LightGray,
                Color.DarkGray);

            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                int x = random.Next(rect.Width);
                int y = random.Next(rect.Height);
                int w = random.Next(m / 50);
                int h = random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }


            // write keyword

            // keyword positioning
            // to space equally
            int posx;
            int posy;
            int deltax;

            if (tempKey.Length == 0)
            {
                tempKey = " ";
            }
            deltax = Convert.ToInt32(size.Width / tempKey.Length);
            posx = Convert.ToInt32((width - size.Width) / 2);

            // write each keyword char
            for (int l = 0; l < tempKey.Length; l++)
            {
                posy = ((int)(2.5 * (bmp.Height / 5))) + (((l % 2) == 0) ? -2 : 2) * ((int)(size.Height / 3));
                posy = (int)((bmp.Height / 2) + (size.Height / 2));
                posy += (int)((((l % 2) == 0) ? -2 : 2) * ((size.Height / 3)));
                posx += deltax;
                g.DrawString(tempKey[l].ToString(),
                    font,
                    fontcolor,
                    posx,
                    posy,
                    sFormat);
            }


            // draw a curve 
            Point[] ps = new Point[points];

            for (int ii = 0; ii < points; ii++)
            {
                int x, y;
                x = random.Next(bmp.Width);
                y = random.Next(bmp.Height);
                ps[ii] = new Point(x, y);
            }
            g.DrawCurve(new Pen(fontcolor, 2), ps, Convert.ToSingle(random.NextDouble()));

            // Clean up.
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            this.image = bmp;
        }
    }
    
    class ImageModifier : ImageBuilder
    {
        private Font font;
        private string fontname = "arial black";
        private float fontsize;
        private FontStyle fontstyle = FontStyle.Italic;
        private Brush fontcolor = Brushes.Black;

        private int points;


        #region Properties
        /// <summary>
        /// Имя шрифта
        /// </summary>
        public string FontName
        {
            get
            {
                return fontname;
            }
        }
        /// <summary>
        /// Размер шрифта
        /// </summary>
        public float FontSize
        {
            get
            {
                return fontsize;
            }
        }
        /// <summary>
        /// Стиль шрифта
        /// </summary>
        public FontStyle FontStyle
        {
            get
            {
                return fontstyle;
            }
        }
        /// <summary>
        /// Кисть
        /// </summary>
        public Brush FontColor
        {
            get
            {
                return fontcolor;
            }
        }

        /// <summary>
        /// Количество точек в кривой
        /// </summary>
        public int Points
        {
            get
            {
                return points;
            }
        }
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Text">Текст</param>
        /// <param name="FileName">Путь к файлу</param>
        public ImageModifier(string Text, string FileName)
        {
            this.text = Text;
            Bitmap bmp = new Bitmap(FileName);
            this.width = bmp.Width;
            this.height = bmp.Height;
            this.GenerateImage(bmp);
        }


        /// <summary>
        /// Генерирование картинки
        /// </summary>
        /// <returns></returns>
        private void GenerateImage(Bitmap bmp)
        {
            Random random = new Random();

            this.points = random.Next(5, 15);
            this.fontsize = random.Next(18, 48);

            StringFormat sFormat = new StringFormat();
            sFormat.Alignment = StringAlignment.Center;
            sFormat.LineAlignment = StringAlignment.Center;

            Graphics g = Graphics.FromImage(bmp);


            // Set up the text font.
            SizeF size;
            float fontSize = fontsize + 1;
            Font tempfont;

            // try to use requested font, but
            // If the named font is not installed, default to a system font.
            try
            {
                tempfont = new Font(fontname, fontsize);
                tempfont.Dispose();
            }
            catch (Exception)
            {
                fontname = System.Drawing.FontFamily.GenericSerif.Name;
            }


            // build a new string with space through the chars
            // i.e. keyword 'hello' become 'h e l l o '
            string tempKey = "";

            for (int ii = 0; ii < text.Length; ii++)
            {
                tempKey = String.Concat(tempKey, text[ii].ToString());
                tempKey = String.Concat(tempKey, " ");
            }

            // Adjust the font size until the text fits within the image.
            do
            {
                fontSize--;
                tempfont = new Font(fontname, fontSize, fontstyle);
                size = g.MeasureString(tempKey, tempfont);
            } while (size.Width > (0.8 * bmp.Width));

            font = tempfont;

            // write keyword

            // keyword positioning
            // to space equally
            int posx;
            int posy;
            int deltax;

            if (tempKey.Length == 0)
            {
                tempKey = " ";
            }
            deltax = Convert.ToInt32(size.Width / tempKey.Length);
            posx = Convert.ToInt32((width - size.Width) / 2);

            // write each keyword char
            for (int l = 0; l < tempKey.Length; l++)
            {
                posy = ((int)(2.5 * (bmp.Height / 5))) + (((l % 2) == 0) ? -2 : 2) * ((int)(size.Height / 3));
                posy = (int)((bmp.Height / 2) + (size.Height / 2));
                posy += (int)((((l % 2) == 0) ? -2 : 2) * ((size.Height / 3)));
                posx += deltax;
                g.DrawString(tempKey[l].ToString(),
                    font,
                    fontcolor,
                    posx,
                    posy,
                    sFormat);
            }


            // draw a curve 
            Point[] ps = new Point[points];

            for (int ii = 0; ii < points; ii++)
            {
                int x, y;
                x = random.Next(bmp.Width);
                y = random.Next(bmp.Height);
                ps[ii] = new Point(x, y);
            }
            g.DrawCurve(new Pen(fontcolor, 2), ps, Convert.ToSingle(random.NextDouble()));

            // Clean up.
            font.Dispose();
            g.Dispose();

            this.image = bmp;
        }
    }
}
