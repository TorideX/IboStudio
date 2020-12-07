using System;
using System.Windows.Forms;
using VisualStudio.Model;
using VisualStudio.View;

namespace VisualStudio.Presenter
{
    class NewProjectPresenter
    {
        INewProjectForm newProjectForm;
        public SolutionClass solution = new SolutionClass();
        
        public IView View { get { return newProjectForm as IView; } }
        public bool SubDir { get; set; }

        public NewProjectPresenter(INewProjectForm _newProjectForm)
        {
            newProjectForm = _newProjectForm;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            newProjectForm.CancelButtonClicked += CancelButtonClicked;
            newProjectForm.OKButtonClicked += OKButtonClicked;
            newProjectForm.ChooseFolderButtonClicked += ChooseFolder;
            newProjectForm.FileNameChanged += FileNameChanged;
            newProjectForm.CreateSubDirChanged += CreateSubDirChanged;
        }

        private void CreateSubDirChanged(object sender, CheckBoxArgs e)
        {
            SubDir = e.isChecked;
        }

        private void FileNameChanged(object sender, FileNameArgs e)
        {
            solution.FileName = e.FileName;
            newProjectForm.IsEverythingOK();
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            View.CloseDialog(false);
        }

        private void ChooseFolder(object sender, EventArgs e)
        {
            string path;
            if(newProjectForm.OpenFolderBrowserDialogBlyad(out path) == true)
            {
                solution.Path = path;
                newProjectForm.ChangeFolderPath(solution.Path);
            }
            newProjectForm.IsEverythingOK();
        }

        private void OKButtonClicked(object sender, EventArgs e)
        {
            View.CloseDialog(true);
        }
    }
}
