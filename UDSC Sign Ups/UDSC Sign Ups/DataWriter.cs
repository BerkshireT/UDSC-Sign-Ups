using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDSC_Sign_Ups
{
    class DataWriter
    {
        public void Create(List<string> data)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\" + data[0] + ", " + data[1] + ".csv";

            if (!File.Exists(filePath))
            {
                try
                {
                    var file = File.Create(filePath);
                    file.Close();
                }
                catch (IOException)
                {
                    Console.WriteLine("File in use.");
                }
            }
        }

        public void Write(string filePath, List<string> data)
        {
            FileInfo file = new FileInfo(filePath);
            StringBuilder csv = new StringBuilder();

            if (file.Length == 0)
            {
                csv.Append("Name, UD Email, Gamertag, Melee, Smash 4");
                csv.Append(Environment.NewLine);
            }

            for (int i = 0; i <= 4; i++)
            {
                csv.Append(data[i] + ",");
            }
            csv.Append(Environment.NewLine);
            
            File.AppendAllText(filePath.ToString(), csv.ToString());
        }
    }
}
