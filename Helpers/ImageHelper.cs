using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PluginCore;
using System.Drawing;

namespace CustomizeToolbar.Helpers
{
    public class ImageHelper
    {
        private static ImageList _images = null;

        /// <summary>
        /// Gets an image list of all the images supplied with FD
        /// </summary>
        public static ImageList Images
        {
            get
            {
                if (_images == null)
                {
                    _images = new ImageList();
                    for (int imageIndex = 0; imageIndex < 545; imageIndex++)
                    {
                        string imageName = imageIndex.ToString();
                        Image image = PluginBase.MainForm.FindImage(imageName);
                        image.Tag = imageName;
                        _images.Images.Add(image);
                    }
                }
                return _images;
            }
        }
    }
}
