using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Diagnostics;

namespace lab1_gui
{
    public partial class Form1 : Form
    {
        TextEditor textEdit = new();
        Scanner scanner = new();
        
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);
            richTextBox1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
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
        private void ñîçäàíèåToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileNew(richTextBox1);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            textEdit.FileNew(richTextBox1);
        }

        private void îòêğûòèåToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ñîõğàíåíèåToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileSave(richTextBox1);
        }
        private void ñîõğàíåíèåÊàêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.file = string.Empty;
            textEdit.FileSave(richTextBox1);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            textEdit.FileUndo(richTextBox1);
        }
        private void îòìåíèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileUndo(richTextBox1);
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            textEdit.FileRedo(richTextBox1);
        }
        private void ïîâòîğèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileRedo(richTextBox1);
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            textEdit.FileCut(richTextBox1);
        }
        private void âûğåçàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileCut(richTextBox1);
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            textEdit.FileCopy(richTextBox1);
        }
        private void êîïèğîâàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileCopy(richTextBox1);
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            textEdit.FilePaste(richTextBox1);
        }
        private void âñòàâèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FilePaste(richTextBox1);
        }
        private void âûõîäToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void óäàëèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileClear(richTextBox1);
        }

        private void âûäåëèòüÂñåToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEdit.FileSelectAll(richTextBox1);
        }

        private void îÏğîãğàììåToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void âûçîâÑïğàâêèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About.AboutInstructions();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = scanner.TextBoxScanner(richTextBox1);
        }
    }
}
