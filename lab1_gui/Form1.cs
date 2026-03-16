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
            dataGridView1.CellClick += dataGridView_CellClick;
        }
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView == null) return;

            var selectedRow = dataGridView.Rows[e.RowIndex];
            var token = selectedRow.DataBoundItem as Token;

            if (token != null)
            {
                if (token.Type == TokenType.Error)
                {
                    richTextBox1.Focus();

                    if (token.AbsoluteIndex >= 0 && token.AbsoluteIndex < richTextBox1.TextLength)
                    {
                        richTextBox1.SelectionStart = token.AbsoluteIndex;

                        if (token.Value != null)
                        {
                            int length = token.Value.Length;
                            if (token.AbsoluteIndex + length <= richTextBox1.TextLength)
                            {
                                richTextBox1.SelectionLength = length;
                            }
                            else
                            {
                                richTextBox1.SelectionLength = 1;
                            }
                        }
                        else
                        {
                            richTextBox1.SelectionLength = 1;
                        }

                        richTextBox1.ScrollToCaret();

                        richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;

                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 1000;
                        timer.Tick += (s, args) =>
                        {
                            richTextBox1.SelectionBackColor = System.Drawing.Color.White;
                            timer.Stop();
                            timer.Dispose();
                        };
                        timer.Start();
                    }
                }
            }
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
            scanner.Run(dataGridView1,richTextBox1);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scanner.Run(dataGridView1, richTextBox1);
        }
    }
}
