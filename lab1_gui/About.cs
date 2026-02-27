using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace lab1_gui
{
    public class About
    {
        public static void AboutProgram()
        {
            MessageBox.Show("Текстовый редактор, который в дальнейшем будет расширен до полноценного языкового процессора для анализа исходного кода.","О программе");
        }
        public static void AboutInstructions()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string htmlFilePath = Path.Combine(path, "instructions.html");
            Process.Start("explorer.exe", htmlFilePath);
        }
    }
}
