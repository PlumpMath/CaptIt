using System;
using System.Drawing;

namespace CaptIt
{
    public class ImageEdit
    {
        public Image image;

        public ImageEdit()
        {
            Bitmap b = new Bitmap(0, 0);
            this.image = b;
        }

        public ImageEdit(Image img)
        {
            this.image = (Image)img.Clone();
        }

        ~ImageEdit()
        {
            image.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ShowForm()
        {
            ImageEditor editor = new ImageEditor(image);
            editor.Show();
        }
    }
}
