using System;
using System.Drawing;
using System.IO;

namespace PhotoFinder
{
    public class DescriptorTools
    {

        #region SCD
        public static double[] CalculateSCDHistogram(FileInfo image)
        {
                Bitmap bitmap = (Bitmap)Image.FromFile(image.FullName);
                return CalculateSCDHistogram(bitmap);
        }

        public static double[] CalculateSCDHistogram(Bitmap bitmap)
        {
            SCD_Descriptor SCD = new SCD_Descriptor();
            SCD.Apply(bitmap, 64, 0);
            return SCD.Norm4BitHistogram;
        }

        public static double CalculateSCDDistance(double[] histogramA, double[] histogramB)
        {
            double result = 0;

            for (int i = 0; i < 64; i++)
            {
                result += Math.Abs(histogramA[i] - histogramB[i]);

            }
            return result;
        }

        #endregion
    }
}
