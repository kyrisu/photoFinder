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
using System.Runtime.Serialization.Formatters.Binary;

namespace PhotoFinder
{
    public static class ExtensionMethods
    {
        public static bool IsImage(this FileInfo fi)
        {
            // add other possible extensions here
            if (!(Path.GetExtension(fi.Extension).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(fi.Extension).Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(fi.Extension).Equals(".png", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(fi.Extension).Equals(".gif", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(fi.Extension).Equals(".tmp", StringComparison.InvariantCultureIgnoreCase)))
                return false;

            try
            {
                Image testImage = Image.FromFile(fi.FullName);
            }
            // when image is corrupted, invalid Image.FromImage() throws OutOfMemoryException - odd but true :)
            catch (OutOfMemoryException)
            {
                return false;
            }

            return true;
        }

        public static byte[] BSerialize(this object obj){
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, obj);
            return stream.ToArray();
        }

        public static object BDeserialize(this byte[] array)
        {
            MemoryStream stream = new MemoryStream(array);
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(stream);
        }

        public static byte[] GetByIndex(this Photo photo, string index)
        {
            switch (index)
            {
                case "SCD":
                    return photo.SCD;
                case "CLD":
                    return photo.CLD;
                case "DCD":
                    return photo.DCD;
                case "EHD":
                    return photo.EHD;
                case "CEDD":
                    return photo.CEDD;
                case "FCTH":
                    return photo.FCTH;
                default:
                    return null;
            }
        }
    }
}
