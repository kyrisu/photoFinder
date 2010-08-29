using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PhotoFinder
{
    static class TempManager
    {
        static List<string> TemporaryFiles = new List<string>();

        static public string GetTempFileName()
        {
            string tempFilename = Path.GetTempFileName();
            TemporaryFiles.Add(tempFilename);
            return tempFilename;
        }

        static public void Dispose()
        {
            foreach (var tempFile in TemporaryFiles)
            {
                try
                {
                    File.Delete(tempFile);
                }
                catch (IOException)
                {
                    //some files are still being used despite releasing the handles, for now we will just skip them
                }
            }
            TemporaryFiles.Clear();
        }
    }
}
