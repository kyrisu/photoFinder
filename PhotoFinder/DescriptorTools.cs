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
    
    
    #region EHD

        private static double[,] QuantTable =
            {{0.010867, 0.057915, 0.099526, 0.144849, 0.195573, 0.260504, 0.358031, 0.530128},
                    {0.012266, 0.069934, 0.125879, 0.182307, 0.243396, 0.314563, 0.411728, 0.564319},
                    {0.004193, 0.025852, 0.046860, 0.068519, 0.093286, 0.123490, 0.161505, 0.228960},
                    {0.004174, 0.025924, 0.046232, 0.067163, 0.089655, 0.115391, 0.151904, 0.217745},
                    {0.006778, 0.051667, 0.108650, 0.166257, 0.224226, 0.285691, 0.356375, 0.450972}};
        protected static int[,] weightMatrix = new int[3, 64];

        public static double[] PoliczEHD(FileInfo image)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(image.FullName);
            return PoliczEHD(bitmap);
        }

        public static double[] PoliczEHD(Bitmap bitmap)
        {
            EHD_Descriptor Mpeg7EHD = new EHD_Descriptor(11);
            double[] SourceEHDTable = new double[80];
            SourceEHDTable = Mpeg7EHD.Apply(bitmap);
            return SourceEHDTable;
        }
        
        public static double PoliczOdlegloscEHD(double[] EHD1, double[] EHD2)
        {
            double result = 0;

            for (int i = 0; i < EHD1.Length; i++)
            {

                result += Math.Abs((float)QuantTable[i % 5, (int)EHD1[i]] - (float)QuantTable[i % 5, (int)EHD2[i]]);
            }
            for (int i = 0; i <= 4; i++)
            {
                result += 5f * Math.Abs((float)EHD1[i] - (float)EHD2[i]);
            }
            for (int i = 5; i < 80; i++)
            {
                result += Math.Abs((float)EHD1[i] - (float)EHD2[i]);
            }
            return result;
        }
    
    #endregion
    }
}
