using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualStudio.View
{
    public partial class NewFileForm : Form, INewFileForm, IView
    {
        public event EventHandler CancelButtonClicked;
        public event EventHandler<FileNameArgs> OKButtonClicked;

        public NewFileForm()
        {
            InitializeComponent();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            CancelButtonClicked.Invoke(sender, e);
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            OKButtonClicked.Invoke(sender, new FileNameArgs() { FileName = textBox1.Text });
        }

        bool IView.ShowDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }

        public void CloseDialog(bool isOK)
        {
            if (isOK)
                DialogResult = DialogResult.OK;
        }

        public void CloseForm()
        {
            this.Close();
        }
    }
}
