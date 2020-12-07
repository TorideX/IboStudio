using FastColoredTextBoxNS;
using System;
using System.Windows.Forms;

namespace VisualStudio.View
{
    public interface IIboStudioForm
    {
        event EventHandler LoadFormEvent;
        event EventHandler<TreeNodeMouseClickEventArgs> TreeViewDoubleClicked;
        event EventHandler CreateProjectEvent;
        event EventHandler OpenProjectEvent;
        event EventHandler NewFileEvent;
        event EventHandler OpenFileEvent;
        event EventHandler<FileArgs> SaveFocusedFileEvent;
        event EventHandler<AllFilesArgs> SaveAllFilesEvent;
        event EventHandler<FileArgs> RemoveFileEvent;
        event EventHandler ExitEvent;

        event EventHandler<CollapsedArgs> ProjectTreeCollapsed;
        event EventHandler<CollapsedArgs> ErrorBlockCollapsed;

        event EventHandler<AllFilesArgs> BuildClickedEvent;
        event EventHandler RunButtonClickedEvent;

        event EventHandler CloseProjectEvent;

        event EventHandler CutEvent;
        event EventHandler CopyEvent;
        event EventHandler PasteEvent;

        bool OpenFileDialogBlyad(out string fileName, out string safeFileName, bool isProject);
        void DisableButtons(bool isEnabled);
        void ClearProject();
        void HideProjectTree();
        void ShowProjectTree();
        void HideErrorBlock();
        void ShowErrorBlock();
        void SetOutputPage(string output);
        void CutText();
        void CopyText();
        void PasteText();
        void ClearErrorPage();
        void CreateSpaceToErrorPage();
        void AddToErrorPage(string code, string message, string line, string file);
        void ClearWarningPage();
        void CreateSpaceToWarningPage();
        void AddToWarningPage(string code, string message, string line, string file);
        void LoadNodeToTreeView(string nodeName, int imageIndex, bool isChild);
        bool IsTabExist(string pageName);
        void AddTab(string tabName, string text);
        void RemoveTab(string pageName);
    }
}
