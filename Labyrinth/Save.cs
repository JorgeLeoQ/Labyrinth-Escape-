using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Labyrinth
{
    public static class Save
    {
        public static void SaveBestTime(string info)
        {
            using(StreamWriter outputFile = new StreamWriter(Path.GetFullPath(C.pathBestTime)))
            {
                outputFile.WriteLine(info);
            }
        }

        public static void LoadBestTime()
        {
            C.bestTime = (int)Convert.ToDouble(File.ReadAllLines(C.pathBestTime).GetValue(0).ToString());
        }
    }
}
