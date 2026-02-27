using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1_gui
{
    public class TextEditor
    {
        public string file = string.Empty;
        public int current_length = 0;
        public bool CommitChanges(RichTextBox r)
        {
            if (r.Text.Length>current_length || r.Text.Length < current_length)
            {
                DialogResult dlg = MessageBox.Show("Сохранить изменения?", "Предупреждение", MessageBoxButtons.YesNo);
                if (dlg == DialogResult.Yes)
                {
                    FileSave(r);
                }
            }
            return true;
        }
        public void FileNew(RichTextBox r)
        {
            if (CommitChanges(r))
            {
                this.file = string.Empty;
                r.Clear();
            }
        }
        public void FileOpen(RichTextBox r, Form f)
        {
            if (CommitChanges(r))
            {
                OpenFileDialog open = new OpenFileDialog();

                open.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
                open.Title = "Открыть";
                open.FileName = "";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    this.file = open.FileName;
                    f.Text = string.Format("{0}", Path.GetFileNameWithoutExtension(open.FileName));
                    StreamReader reader = new StreamReader(open.FileName);
                    r.Text = reader.ReadToEnd();
                    reader.Close();
                }
            }
        }
        public void FileSave(RichTextBox r)
        {
            if (string.IsNullOrEmpty(this.file))
            {
                SaveFileDialog saving = new SaveFileDialog();

                saving.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                saving.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
                saving.Title = "Сохранить как";
                saving.FileName = "Безымянный";

                if (saving.ShowDialog() == DialogResult.OK)
                {
                    file = saving.FileName;
                    StreamWriter writing = new StreamWriter(saving.FileName);
                    current_length = r.Text.Length;
                    writing.Write(r.Text);
                    writing.Close();
                }
            }
            else
            {
                StreamWriter writer = new StreamWriter(file);
                current_length = r.Text.Length;
                writer.Write(r.Text);
                writer.Close();
            }
        }
        public void FileUndo(RichTextBox r)
        {
            if (r.CanUndo)
            {
                r.Undo();
            }
        }
        public void FileRedo(RichTextBox r)
        {
            if (r.CanRedo)
            {
                if (r.RedoActionName != "Delete")
                    r.Redo();
            }
        }
        public void FileCut(RichTextBox r)
        {
            if (r.SelectedText.Length > 0) r.Cut();
        }
        public void FileCopy(RichTextBox r)
        {
            if (r.SelectedText.Length > 0) r.Copy();
        }
        public void FilePaste(RichTextBox r)
        {
            r.Paste();
        }
        public void FileClear(RichTextBox r)
        {
            r.Clear();
        }
        public void FileSelectAll(RichTextBox r)
        {
            r.SelectAll();
        }
    }
}
