using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NasiPadang
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    class ReadFile
    {
        static void readFromFile(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            foreach(string line in lines)
            {

            }
        }
    }
}
