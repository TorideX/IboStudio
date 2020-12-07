using System;

namespace VisualStudio.View
{
    interface INewProjectForm
    {
        event EventHandler<FileNameArgs> FileNameChanged;
        event EventHandler<CheckBoxArgs> CreateSubDirChanged;
        event EventHandler ChooseFolderButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler OKButtonClicked;
        void IsEverythingOK();
        void ChangeFolderPath(string path);
        bool OpenFolderBrowserDialogBlyad(out string path);
    }
}
