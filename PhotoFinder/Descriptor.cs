using System;

namespace PhotoFinder
{
    [Flags]
    enum Descriptor
    {
        NONE = 0,    // uknown or not set descriptor
        CLD = 1,        // Color Layout Descriptor
        DCD = 2,        // Dominant Color Descriptor
        EHD = 4,        // Edge Histogram Descriptor
        SCD = 8,        // Scalable Color Descriptor
        CEDD = 16,      // Color and Edge Directivity Descriptor
        FCTH = 32       // Fuzzy Color and Texture Histogram
    }
}
