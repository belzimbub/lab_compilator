using lab_compilator;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace lab1_gui
{
    public partial class Form1 : Form
    {
        private RegexClass regex;
        TextEditor textEdit = new();
        Scanner scanner = new();
        Parser parser = new();

        public Form1()
        {
            InitializeComponent();
            regex = new RegexClass(
                richTextBox1,
                dataGridView1,
                comboBox1,
                label1
            );
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);
            richTextBox1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
        }
        private void Run()
        {
            List<Token> tokens = scanner.Analyze(richTextBox1.Text);
            parser.Parse(tokens);
            parser.Display(dataGridView1, label1,richTextBox1);
            //scanner.Run(dataGridView1,richTextBox1);
            //regex.PerformSearch();
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            textEdit.textWasChanged = true;
        }
        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            if (textEdit.CommitChanges(richTextBox1))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileNew(richTextBox1);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            textEdit.FileNew(richTextBox1);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileOpen(richTextBox1, this);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            textEdit.FileOpen(richTextBox1, this);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            textEdit.FileSave(richTextBox1);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileSave(richTextBox1);
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.file = string.Empty;
            textEdit.FileSave(richTextBox1);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            textEdit.FileUndo(richTextBox1);
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileUndo(richTextBox1);
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            textEdit.FileRedo(richTextBox1);
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileRedo(richTextBox1);
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            textEdit.FileCut(richTextBox1);
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileCut(richTextBox1);
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            textEdit.FileCopy(richTextBox1);
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileCopy(richTextBox1);
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            textEdit.FilePaste(richTextBox1);
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FilePaste(richTextBox1);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileClear(richTextBox1);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileSelectAll(richTextBox1);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About.AboutProgram();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            About.AboutInstructions();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            About.AboutProgram();
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About.AboutInstructions();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Run();
        }
    }
}
