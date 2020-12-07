using System;
using VisualStudio.View;

namespace VisualStudio.Presenter
{
    class NewFilePresenter
    {
        INewFileForm form;
        public string FileName { get; set; }

        public IView View { get { return form as IView; } }

        public NewFilePresenter(INewFileForm _form)
        {
            form = _form;
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            form.CancelButtonClicked += CancelButtonClicked;
            form.OKButtonClicked += OKButtonClicked;
        }

        private void OKButtonClicked(object sender, FileNameArgs e)
        {
            FileName = e.FileName;
            View.CloseDialog(FileName != "");
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            form.CloseForm();
        }
    }
}
