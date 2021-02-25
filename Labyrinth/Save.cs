using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Labyrinth
{
    public static class Save
    {
        private static string pathBestTime = @"C:\Users\Jorge\Desktop\Labyrinth\Labyrinth-Escape-\Labyrinth\Content\BestScore.txt";
        public static void SaveBestTime(string info)
        {
            using(StreamWriter outputFile = new StreamWriter(Path.GetFullPath(pathBestTime)))
            {
                outputFile.WriteLine(info);
            }
        }

        public static void LoadBestTime()
        {
            C.bestTime = (int)Convert.ToDouble(File.ReadAllLines(pathBestTime).GetValue(0).ToString());
        }
    }
}
