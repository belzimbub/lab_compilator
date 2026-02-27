using System.Diagnostics;
using System.IO;

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
            string htmlFilePath = @"https://github.com/belzimbub/lab1_gui/blob/master/README.md";
            Process.Start("explorer.exe", htmlFilePath);
        }
    }
}
