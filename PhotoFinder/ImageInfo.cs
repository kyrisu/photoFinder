using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoFinder
{
    class ImageInfo
    {
        public string PhotoID;
        public double CLDDiff;
        public double DCDDiff;
        public double EHDDiff;
        public double SCDDiff;
        public double CEDDDiff;
        public double FCTHDiff;

        public void SetDescriptorDistance(Descriptor desc, double distance)
        {
            switch (desc)
            {
                case Descriptor.NONE:
                    break;
                case Descriptor.CLD:
                    CLDDiff = distance;
                    break;
                case Descriptor.DCD:
                    DCDDiff = distance;
                    break;
                case Descriptor.EHD:
                    EHDDiff = distance;
                    break;
                case Descriptor.SCD:
                    SCDDiff = distance;
                    break;
                case Descriptor.CEDD:
                    CEDDDiff = distance;
                    break;
                case Descriptor.FCTH:
                    FCTHDiff = distance;
                    break;
                default:
                    break;
            }
        }

        public double GetDescriptorDistance(Descriptor desc)
        {
            switch (desc)
            {
                case Descriptor.NONE:
                    return 0.0;
                case Descriptor.CLD:
                    return CLDDiff;
                case Descriptor.DCD:
                    return DCDDiff;
                case Descriptor.EHD:
                    return EHDDiff;
                case Descriptor.SCD:
                    return SCDDiff;
                case Descriptor.CEDD:
                    return CEDDDiff;
                case Descriptor.FCTH:
                    return FCTHDiff;
                default:
                    return 0.0;
            }
        }
    }
}
