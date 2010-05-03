using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Gallery = Manina.Windows.Forms;

namespace PhotoFinder
{
    public static class ExtensionMethods
    {
        public static bool IsImage(this FileInfo fi)
        {
            // add other possible extensions here
            return Path.GetExtension(fi.Extension).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(fi.Extension).Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(fi.Extension).Equals(".png", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(fi.Extension).Equals(".gif", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
