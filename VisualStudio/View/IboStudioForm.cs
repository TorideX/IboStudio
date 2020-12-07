using System;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.CodeDom.Compiler;
using System.Diagnostics;
using VisualStudio.View;
using System.Collections.Generic;

namespace VisualStudio
{
    public partial class IboStudioForm : Form, IIboStudioForm
    {
        public event EventHandler LoadFormEvent;
        public event EventHandler<TreeNodeMouseClickEventArgs> TreeViewDoubleClicked;
        public event EventHandler CreateProjectEvent;
        public event EventHandler OpenProjectEvent;
        public event EventHandler NewFileEvent;
        public event EventHandler OpenFileEvent;
        public event EventHandler<FileArgs> SaveFocusedFileEvent;
        public event EventHandler<AllFilesArgs> SaveAllFilesEvent;
        public event EventHandler<FileArgs> RemoveFileEvent;
        public event EventHandler ExitEvent;

        public event EventHandler<CollapsedArgs> ProjectTreeCollapsed;
        public event EventHandler<CollapsedArgs> ErrorBlockCollapsed;

        public event EventHandler<AllFilesArgs> BuildClickedEvent;
        public event EventHandler RunButtonClickedEvent;

        public event EventHandler CloseProjectEvent;
        
        public event EventHandler CutEvent;
        public event EventHandler CopyEvent;
        public event EventHandler PasteEvent;


        int errorIndex = 1;
        int warningIndex = 1;

        public IboStudioForm()
        {
            InitializeComponent();
        }
        
        public bool OpenFileDialogBlyad(out string fileName, out string safeFileName, bool isProject)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (isProject == true)
                dialog.Filter = "IboStudio files|*.iSln";
            else
                dialog.Filter = "CS files|*.cs";

            fileName = "";
            safeFileName = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
                safeFileName = dialog.SafeFileName;
                return true;
            }
            return false;
        }

        public void PasteText()
        {
            (tab_CodeFiles.SelectedTab.Controls[0] as FastColoredTextBox).Paste();
        }
        public void CopyText()
        {
            (tab_CodeFiles.SelectedTab.Controls[0] as FastColoredTextBox).Copy();
        }
        public void CutText()
        {
            (tab_CodeFiles.SelectedTab.Controls[0] as FastColoredTextBox).Cut();
        }

        public void HideProjectTree()
        {
            projectTreeCollapsedToolStripMenuItem.Image = CollapseImg.Images[0];
            splitContainer1.Panel1Collapsed = true;
        }
        public void ShowProjectTree()
        {
            projectTreeCollapsedToolStripMenuItem.Image = null;
            splitContainer1.Panel1Collapsed = false;
        }
        public void HideErrorBlock()
        {
            errorToolStripMenuItem.Image = CollapseImg.Images[0];
            tableLayoutPanel1.SetRowSpan(splitContainer1, 2);
            tableLayoutPanel_Error.Hide();
        }
        public void ShowErrorBlock()
        {
            errorToolStripMenuItem.Image = null;
            tableLayoutPanel1.SetRowSpan(splitContainer1, 1);
            tableLayoutPanel_Error.Show();
        }

        public void DisableButtons(bool isEnabled)
        {
            ts_Cut.Enabled = isEnabled;
            ts_Copy.Enabled = isEnabled;
            ts_Paste.Enabled = isEnabled;
            ts_SaveFile.Enabled = isEnabled;
            ts_SaveAllFiles.Enabled = isEnabled;
            ts_Build.Enabled = isEnabled;
            ts_Run.Enabled = isEnabled;
            ts_Comment.Enabled = isEnabled;
            addNewFileToolStripMenuItem.Enabled = isEnabled;
            runToolStripMenuItem.Enabled = isEnabled;
            buildToolStripMenuItem.Enabled = isEnabled;
            closeProjectToolStripMenuItem.Enabled = isEnabled;
            cutToolStripMenuItem.Enabled = isEnabled;
            copyToolStripMenuItem.Enabled = isEnabled;
            pasteToolStripMenuItem.Enabled = isEnabled;
            saveFocusedFileToolStripMenuItem.Enabled = isEnabled;
            saveAllFilesToolStripMenuItem.Enabled = isEnabled;
        }
        public void ClearProject()
        {
            treeView_Solution.Nodes.Clear();
            tab_CodeFiles.TabPages.Clear();
            ClearErrorPage();
            ClearWarningPage();
            textBox_Output.Text = "";
        }

        public bool IsTabExist(string pageName)
        {
            bool isExist = false;
            foreach (TabPage item in tab_CodeFiles.TabPages)
            {
                if(item.Text == pageName)
                {
                    tab_CodeFiles.SelectedTab = item;
                    isExist = true;
                    break;
                }
            }
            return isExist;
        }
        public void RemoveTab(string pageName)
        {
            int index = 0;
            foreach (TabPage item in tab_CodeFiles.TabPages)
            {
                if (item.Text == pageName)
                    break;
                index++;
            }

            tab_CodeFiles.TabPages.RemoveAt(index);
        }

        public void SetOutputPage(string output)
        {
            textBox_Output.Text = output;
        }
        public void ClearErrorPage()
        {
            tableLayoutPanel_Error.Controls.Clear();
            errorIndex = 1;
            tableLayoutPanel_Error.Controls.Add(new Label() { Text = "Code", Dock = DockStyle.Bottom});
            tableLayoutPanel_Error.Controls.Add(new Label() { Text = "Message", Dock = DockStyle.Bottom});
            tableLayoutPanel_Error.Controls.Add(new Label() { Text = "Line", Dock = DockStyle.Bottom});
            tableLayoutPanel_Error.Controls.Add(new Label() { Text = "File", Dock = DockStyle.Bottom});
        }
        public void CreateSpaceToErrorPage()
        {
            if (errorIndex == tableLayoutPanel_Error.RowCount - 1)
                tableLayoutPanel_Error.RowStyles.Add(new RowStyle(SizeType.Absolute));
        }
        public void AddToErrorPage(string code, string message, string line, string file)
        {
            tableLayoutPanel_Error.Controls.Add(new Label() { Text = code, Dock = DockStyle.Fill }, 0, errorIndex);
            tableLayoutPanel_Error.Controls.Add(new Label() { Text = message, Dock = DockStyle.Fill }, 1, errorIndex);
            tableLayoutPanel_Error.Controls.Add(new Label() { Text = line, Dock = DockStyle.Fill }, 2, errorIndex);
            tableLayoutPanel_Error.Controls.Add(new Label() { Text = file, Dock = DockStyle.Fill }, 3, errorIndex);
            errorIndex++;            
        }

        public void ClearWarningPage()
        {
            tableLayoutPanel_Warning.Controls.Clear();
            warningIndex = 1;
            tableLayoutPanel_Warning.Controls.Add(new Label() { Text = "Code", Dock = DockStyle.Bottom });
            tableLayoutPanel_Warning.Controls.Add(new Label() { Text = "Message", Dock = DockStyle.Bottom });
            tableLayoutPanel_Warning.Controls.Add(new Label() { Text = "Line", Dock = DockStyle.Bottom });
            tableLayoutPanel_Warning.Controls.Add(new Label() { Text = "File", Dock = DockStyle.Bottom });
        }
        public void CreateSpaceToWarningPage()
        {
            if (warningIndex == tableLayoutPanel_Warning.RowCount - 1)
                tableLayoutPanel_Warning.RowStyles.Add(new RowStyle(SizeType.Absolute));
        }
        public void AddToWarningPage(string code, string message, string line, string file)
        {
            tableLayoutPanel_Warning.Controls.Add(new Label() { Text = code, Dock = DockStyle.Fill }, 0, errorIndex);
            tableLayoutPanel_Warning.Controls.Add(new Label() { Text = message, Dock = DockStyle.Fill }, 1, errorIndex);
            tableLayoutPanel_Warning.Controls.Add(new Label() { Text = line, Dock = DockStyle.Fill }, 2, errorIndex);
            tableLayoutPanel_Warning.Controls.Add(new Label() { Text = file, Dock = DockStyle.Fill }, 3, errorIndex);
            warningIndex++;
        }

        public void AddTab(string tabName, string text)
        {
            TabPage tab = new TabPage(tabName);
            FastColoredTextBox textBox = new FastColoredTextBox();
            textBox.Language = Language.CSharp;
            textBox.AutoCompleteBrackets = true;
            textBox.Dock = DockStyle.Fill;
            textBox.Text = text;

            tab_CodeFiles.TabPages.Add(tab);
            tab.Controls.Add(textBox);
            textBox.ContextMenuStrip = TextBoxMenuStrip;
            tab_CodeFiles.SelectedTab = tab;
        }

        public void LoadNodeToTreeView(string nodeName, int imageIndex,  bool isChild)
        {
            TreeNode node = new TreeNode();
            node.Text = nodeName;
            node.ImageIndex = imageIndex;
            node.SelectedImageIndex = imageIndex;

            if (isChild == true)
            {
                node.ContextMenuStrip = ChildMenuStrip;
                treeView_Solution.Nodes[0].Nodes.Add(node);
            }
            else
            {
                node.ContextMenuStrip = NodeMenuStrip;
                treeView_Solution.Nodes.Clear();
                treeView_Solution.Nodes.Add(node);                
            }
            treeView_Solution.ExpandAll();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitEvent.Invoke(sender, e);
        }

        private void createProject_Click(object sender, EventArgs e)
        {
            CreateProjectEvent.Invoke(sender, e);
        }

        private void treeView_Solution_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewDoubleClicked.Invoke(sender, e);
        }

        private void openProject_Click(object sender, EventArgs e)
        {
            OpenProjectEvent.Invoke(sender, e);
        }

        private void newFile_Click(object sender, EventArgs e)
        {
            NewFileEvent.Invoke(sender, e);
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileEvent.Invoke(sender, e);
        }

        private void ts_Build_Click(object sender, EventArgs e)
        {
            List<FileAndContent> files = new List<FileAndContent>();
            foreach (TabPage item in tab_CodeFiles.TabPages)
            {
                files.Add(new FileAndContent() { FileName = item.Text, Content = item.Controls[0].Text });
            }
            BuildClickedEvent.Invoke(sender, new AllFilesArgs() { Files = files });
        }

        private void ts_Run_Click(object sender, EventArgs e)
        {
            RunButtonClickedEvent.Invoke(sender, e);
        }

        private void ts_SaveFile_Click(object sender, EventArgs e)
        {
            SaveFocusedFileEvent.Invoke(sender, new FileArgs() { FileName = tab_CodeFiles.SelectedTab.Text, Content = tab_CodeFiles.SelectedTab.Controls[0].Text });
        }

        private void ts_SaveAllFiles_Click(object sender, EventArgs e)
        {
            List<FileAndContent> files = new List<FileAndContent>();
            foreach (TabPage item in tab_CodeFiles.TabPages)
            {
                files.Add(new FileAndContent() { FileName = item.Text, Content = item.Controls[0].Text });
            }
            SaveAllFilesEvent.Invoke(sender, new AllFilesArgs() { Files = files });
        }

        private void ts_Undo_Click(object sender, EventArgs e)
        {
            (tab_CodeFiles.SelectedTab.Controls[0] as FastColoredTextBox).Undo();
        }

        private void ts_Redo_Click(object sender, EventArgs e)
        {
            (tab_CodeFiles.SelectedTab.Controls[0] as FastColoredTextBox).Redo();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            (tab_CodeFiles.SelectedTab.Controls[0] as FastColoredTextBox).CommentSelected();
        }

        private void ts_Cut_Click(object sender, EventArgs e)
        {
            CutEvent.Invoke(sender, e);
        }

        private void ts_Copy_Click(object sender, EventArgs e)
        {
            CopyEvent.Invoke(sender, e);
        }

        private void ts_Paste_Click(object sender, EventArgs e)
        {
            PasteEvent.Invoke(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This App Created by Ibrahim H\nApp called: IboStudio\nContact us: IboStudioOfficial@gmail.com\n© All rights reserved", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void projectTreeCollapsedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectTreeCollapsed.Invoke(sender, new CollapsedArgs() { IsCollapsed = (projectTreeCollapsedToolStripMenuItem.Image != null) });
        }

        private void errorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErrorBlockCollapsed.Invoke(sender, new CollapsedArgs() { IsCollapsed = (errorToolStripMenuItem.Image != null) });
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseProjectEvent.Invoke(sender, e);
        }

        private void IboStudioForm_Load(object sender, EventArgs e)
        {
            LoadFormEvent.Invoke(sender, e);
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tab_CodeFiles.TabPages.Remove(tab_CodeFiles.SelectedTab);
        }

        private void removeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveFileEvent.Invoke(sender, new FileArgs() { FileName = treeView_Solution.SelectedNode.Text });
        }
    }
}
