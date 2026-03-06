using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1_gui
{
    public class Scanner
    {
        public List<string> TextBoxScanner(RichTextBox r)
        {
            var lines = r.Text.Split(' ').ToList();
            return lines;
        }
    }
}
