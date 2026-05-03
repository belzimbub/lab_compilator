using lab_compilator;
using System.Data;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace lab1_gui
{
    public partial class Form1 : Form
    {
        TextEditor textEdit = new();

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);
            richTextBox1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
        }
        private void Run()
        {
            Scanner scanner = new();
            Parser parser = new();
            List<Token> tokens = scanner.Analyze(richTextBox1.Text);
            parser.Parse(tokens);
            if (parser.GetErrors().Count == 0)
            {
                SemanticAnalyzer semanticAnalyzer = new();
                ProgramNode ast = semanticAnalyzer.Analyze(tokens);

                if (semanticAnalyzer.HasErrors())
                {
                    semanticAnalyzer.DisplaySemanticErrors(semanticAnalyzer.GetSemanticErrors(), dataGridView1, label1);
                }
                else
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();
                    label1.Text = "";
                    AstForm astForm = new AstForm(ast);
                    astForm.ShowDialog();
                }
            }
            else
            {
                parser.Display(dataGridView1, label1, richTextBox1);
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
            Run();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выполнить программную реализацию алгоритма синтаксического анализа целочисленных констант с инициализацией на языке PASCAL.", "Постановка задачи", MessageBoxButtons.OK);
        }

        private void grammarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) <Start> → “const” <BeginID>\r\n\r\n2) <BeginID> → letter<ID> | “_”<ID>\r\n\r\n3) <ID> → letter<ID> | “_”<ID> | digit<ID> | “:” <Keyword>\r\n\r\n4) <Keyword> → “integer”<Equal>\r\n\r\n5) <Equal> → “=”<Integer>\r\n\r\n6) <Integer> → “+”<UnsignedInteger>| “-”<UnsignedInteger> | digit<End> | digit<UnsignedInteger>\r\n\r\n7) <UnsignedInteger> → digit<UnsignedInteger> | digit<End>\r\n\r\n8) <End> → “;”.", "Грамматика", MessageBoxButtons.OK);
        }

        private void gramClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Согласно классификации Хомского, грамматика G[Z] является автоматной. Правила относятся к классу праворекурсивных продукций (A → aB | a | ε).", "Классификация грамматики", MessageBoxButtons.OK);
        }

        private void methodAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMethod form = new FormMethod();
            form.ShowDialog();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "const n4: integer = 52;";
        }

        private void literatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Шорников Ю.В. Теория и практика языковых процессоров : учеб. пособие / Ю.В. Шорников. – Новосибирск: Изд-во НГТУ, 2004.]\n\n2. Gries D.Designing Compilers for Digital Computers. New York, Jhon Wiley, 1971. 493 p.\n\n3. Теория формальных языков и компиляторов[Электронный ресурс] / Электрон.дан.URL: https://dispace.edu.nstu.ru/didesk/course/show/8594, свободный. Яз.рус. (дата обращения 01.04.2021).", "Список литературы", MessageBoxButtons.OK);
        }
    }
}
