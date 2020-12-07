using System;
using System.Windows.Forms;

namespace VisualStudio.View
{
    public partial class NewProjectForm : Form, INewProjectForm, IView
    {
        public event EventHandler<FileNameArgs> FileNameChanged;
        public event EventHandler<CheckBoxArgs> CreateSubDirChanged;
        public event EventHandler ChooseFolderButtonClicked;
        public event EventHandler CancelButtonClicked;
        public event EventHandler OKButtonClicked;

        public NewProjectForm()
        {
            InitializeComponent();
        }

        bool IView.ShowDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }

        public bool OpenFolderBrowserDialogBlyad(out string path)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            path = "";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                path = fileDialog.SelectedPath;
                return true;
            }
            return false;
        }

        public void CloseDialog(bool isOK)
        {
            if (isOK)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Abort;
        }

        public void IsEverythingOK()
        {
            if (textBox_Name.Text != "" && textBox_Folder.Text != "")
                Button_OK.Enabled = true;
            else
                Button_OK.Enabled = false;
        }

        public void ChangeFolderPath(string path)
        {
            textBox_Folder.Text = path;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            CancelButtonClicked.Invoke(sender, e);
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            OKButtonClicked.Invoke(sender, e);
        }

        private void Button_ChooseFolder_Click(object sender, EventArgs e)
        {
            ChooseFolderButtonClicked.Invoke(sender, e);
        }

        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {
            FileNameChanged.Invoke(sender, new FileNameArgs() { FileName = textBox_Name.Text });
        }

        private void checkBox_CreateSubDir_CheckedChanged(object sender, EventArgs e)
        {
            CreateSubDirChanged.Invoke(sender, new CheckBoxArgs() { isChecked = checkBox_CreateSubDir.Checked });
        }
    }
}
