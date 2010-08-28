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

        public static double CalculateSCDDeviation(object ohistogramA, object ohistogramB)
        {
            double[] histogramA = (double[])ohistogramA;
            double[] histogramB = (double[])ohistogramB;
            double result = 0;

            for (int i = 0; i < 64; i++)
            {
                result += Math.Abs(((double[])histogramA)[i] - ((double[])histogramB)[i]);

            }
            return result;
        }

        #endregion

        #region FCTH

        public static double[] CalculateFCTHescriptor(FileInfo image)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(image.FullName);
            return CalculateFCTHescriptor(bitmap);
        }

        public static double[] CalculateFCTHescriptor(Bitmap bitmap)
        {
            FCTH_Descriptor.FCTH FCTH = new FCTH_Descriptor.FCTH();

            return FCTH.Apply(bitmap, 2);

        }

        /// <summary>
        /// Calculating CEDD deviation using Tonimoto Classifier
        /// </summary>
        /// <param name="histogramA"></param>
        /// <param name="histogramB"></param>
        /// <returns></returns>
        public static double CalculateFCTHDeviation(object ohistogramA, object ohistogramB)
        {
            double[] histogramA = (double[]) ohistogramA;
            double[] histogramB = (double[]) ohistogramB;
            double Result = 0;
            double Temp1 = 0;
            double Temp2 = 0;

            double TempCount1 = 0, TempCount2 = 0, TempCount3 = 0;

            for (int i = 0; i < histogramA.Length; i++)
            {
                Temp1 += histogramA[i];
                Temp2 += histogramB[i];
            }

            if (Temp1 == 0 || Temp2 == 0) Result = 100;
            if (Temp1 == 0 && Temp2 == 0) Result = 0;

            if (Temp1 > 0 && Temp2 > 0)
            {
                for (int i = 0; i < histogramA.Length; i++)
                {
                    TempCount1 += (histogramA[i] / Temp1) * (histogramB[i] / Temp2);
                    TempCount2 += (histogramB[i] / Temp2) * (histogramB[i] / Temp2);
                    TempCount3 += (histogramA[i] / Temp1) * (histogramA[i] / Temp1);

                }

                Result = (100 - 100 * (TempCount1 / (TempCount2 + TempCount3 - TempCount1))); //Tanimoto
            }

            return (Result);
        }
        #endregion

        #region CEDD

        public static double[] CalculateCEDDescriptor(FileInfo image)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(image.FullName);
            return CalculateCEDDescriptor(bitmap);
        }

        public static double[] CalculateCEDDescriptor(Bitmap bitmap)
        {
            CEDD_Descriptor.CEDD CEDD = new CEDD_Descriptor.CEDD();
            return CEDD.Apply(bitmap);
        }

        /// <summary>
        /// Calculating CEDD deviation using Tonimoto Classifier
        /// </summary>
        /// <param name="histogramA"></param>
        /// <param name="histogramB"></param>
        /// <returns></returns>
        public static double CalculateCEDDDeviation(object ohistogramA, object ohistogramB)
        {
            double[] histogramA = (double[])ohistogramA;
            double[] histogramB = (double[])ohistogramB;
            double Result = 0;
            double Temp1 = 0;
            double Temp2 = 0;

            double TempCount1 = 0, TempCount2 = 0, TempCount3 = 0;

            for (int i = 0; i < histogramA.Length; i++)
            {
                Temp1 += histogramA[i];
                Temp2 += histogramB[i];
            }

            if (Temp1 == 0 || Temp2 == 0) Result = 100;
            if (Temp1 == 0 && Temp2 == 0) Result = 0;

            if (Temp1 > 0 && Temp2 > 0)
            {
                for (int i = 0; i < histogramA.Length; i++)
                {
                    TempCount1 += (histogramA[i] / Temp1) * (histogramB[i] / Temp2);
                    TempCount2 += (histogramB[i] / Temp2) * (histogramB[i] / Temp2);
                    TempCount3 += (histogramA[i] / Temp1) * (histogramA[i] / Temp1);

                }

                Result = (100 - 100 * (TempCount1 / (TempCount2 + TempCount3 - TempCount1))); //Tanimoto
            }

            return (Result);
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

        public static double PoliczOdlegloscEHD(object oEHD1, object oEHD2)
        {
            double[] EHD1 = (double[])oEHD1;
            double[] EHD2 = (double[])oEHD2;
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

        #region CLD


        
       // int[] ZrodloYCDL = new int[6];
       // int[] ZrodloCbCDL = new int[3];
       // int[] ZrodloCrCDL = new int[3];
       

        public static double[,] PoliczCLD(FileInfo image)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(image.FullName);
            return PoliczCLD(bitmap);
        }

        public static double[,] PoliczCLD(Bitmap bitmap)
        {
            CLD_Descriptor Mpeg7CLD = new CLD_Descriptor();

            double[,] temp= new double[3,64];

            Mpeg7CLD.Apply(bitmap);
            for (int h = 0;h <64; h++)
            {
                temp[0,h] = Mpeg7CLD.YCoeff[h]; // skladowa luminancji
                temp[1,h] = Mpeg7CLD.CbCoeff[h]; // skladowa roznicowa
                temp[2,h] = Mpeg7CLD.CrCoeff[h]; // skladowa chrominancji
                
            }
            return temp;
        }

        private static void setWeightingValues()
        {
            weightMatrix[0, 0] = 2;
            weightMatrix[0, 1] = weightMatrix[0, 2] = 2;
            weightMatrix[1, 0] = 2;
            weightMatrix[1, 1] = weightMatrix[1, 2] = 1;
            weightMatrix[2, 0] = 4;
            weightMatrix[2, 1] = weightMatrix[2, 2] = 2;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 3; j < 64; j++)
                    weightMatrix[i, j] = 1;
            }
        }

        public static double policzodlegloscCLD( object oCLD1, object oCLD2)
        {
            int[,] CLD1 = (int[,])oCLD1;
            int[,] CLD2 = (int[,])oCLD2;
            int[] YCoeff1 = new int[64];
            int[] CbCoeff1 = new int[64];
            int[] CrCoeff1 = new int[64];
            int[] YCoeff2 = new int[64];
            int[] CbCoeff2 = new int[64];
            int[] CrCoeff2 = new int[64];

            for (int h = 0; h < 64; h++)
            {
                YCoeff1[h] = CLD1[0,h]; 
                CbCoeff1[h] = CLD1[1,h]; 
                CrCoeff1[h] = CLD1[2,h]; 
                YCoeff2[h] = CLD2[0, h]; 
                CbCoeff2[h] = CLD2[1, h]; 
                CrCoeff2[h] = CLD2[2, h]; 

            }
            int numYCoeff1, numYCoeff2, CCoeff1, CCoeff2, YCoeff, CCoeff;
            //Numbers of the Coefficients of two descriptor values.
            numYCoeff1 = YCoeff1.Length;
            numYCoeff2 = YCoeff2.Length;
            CCoeff1 = CbCoeff1.Length;
            CCoeff2 = CbCoeff2.Length;
            //take the Minimal Coeff-number
            YCoeff = Math.Min(numYCoeff1, numYCoeff2);
            CCoeff = Math.Min(CCoeff1, CCoeff2);
            setWeightingValues();
            int j;
            int[] sum = new int[3];
            int diff;
            sum[0] = 0;
            for (j = 0; j < YCoeff; j++)
            {
                diff = (YCoeff1[j] - YCoeff2[j]);
                sum[0] += (weightMatrix[0, j] * diff * diff);
            }
            sum[1] = 0;
            for (j = 0; j < CCoeff; j++)
            {
                diff = (CbCoeff1[j] - CbCoeff2[j]);
                sum[1] += (weightMatrix[1, j] * diff * diff);
            }
            sum[2] = 0;
            for (j = 0; j < CCoeff; j++)
            {
                diff = (CrCoeff1[j] - CrCoeff2[j]);
                sum[2] += (weightMatrix[2, j] * diff * diff);
            }
            //returns the distance between the two desciptor values
            return Math.Sqrt(sum[0] * 1.0) + Math.Sqrt(sum[1] * 1.0) + Math.Sqrt(sum[2] * 1.0);
        }


        #endregion


        internal static object CalculateDescriptor(FileInfo file, Descriptor descriptor)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(file.FullName);
            return CalculateDescriptor(bitmap, descriptor);
        }

        internal static object CalculateDescriptor(Bitmap _QueryBitmap, Descriptor descriptor)
        {
            object result = null;
            
            switch (descriptor)
            {
                case Descriptor.CLD:
                    
 
                    result = PoliczCLD(_QueryBitmap);
                    break;
                case Descriptor.DCD:
                    throw new NotImplementedException();
                    break;
                case Descriptor.EHD:
                    result = PoliczEHD(_QueryBitmap);
                    break;
                case Descriptor.SCD:
                    result = CalculateSCDHistogram(_QueryBitmap);
                    break;
                case Descriptor.CEDD:
                    result = CalculateCEDDescriptor(_QueryBitmap);
                    break;
                case Descriptor.FCTH:
                    result = CalculateFCTHescriptor(_QueryBitmap);
                    break;
            }

            return result;
        }

        internal static double CalculateDescriptorDistance(object descA, object descB, Descriptor descriptor)
        {
            double result = 0.0;
            switch (descriptor)
            {
                case Descriptor.CLD:
                    result = policzodlegloscCLD(descA, descB);
                    break;
                case Descriptor.DCD:
                    throw new NotImplementedException();
                    break;
                case Descriptor.EHD:
                    result = PoliczOdlegloscEHD(descA, descB);
                    break;
                case Descriptor.SCD:
                    result = CalculateSCDDeviation(descA, descB);
                    break;
                case Descriptor.CEDD:
                    result = CalculateCEDDDeviation(descA, descB);
                    break;
                case Descriptor.FCTH:
                    result = CalculateFCTHDeviation(descA, descB);
                    break;
            }

            return result;
        }
    }
}
