using FastColoredTextBoxNS;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using VisualStudio.Model;
using VisualStudio.View;
using Microsoft.CSharp;
using System.IO;

namespace VisualStudio.Presenter
{
    class IboStudioPresenter
    {
        private IIboStudioForm iboStudioForm;
        public IIboStudioForm View { get { return iboStudioForm; } }
        ISolutionService service;

        public IboStudioPresenter(IIboStudioForm _iboStudioForm, ISolutionService _service)
        {
            iboStudioForm = _iboStudioForm;
            service = _service;            
            SubscribeEvents();                    
        }

        private void SubscribeEvents()
        {
            iboStudioForm.LoadFormEvent += LoadForm;
            iboStudioForm.CreateProjectEvent += CreateProject;
            iboStudioForm.OpenProjectEvent += OpenProject;
            iboStudioForm.ExitEvent += Exit;
            iboStudioForm.NewFileEvent += CreateFile;
            iboStudioForm.TreeViewDoubleClicked += TreeViewDoubleClicked;
            iboStudioForm.OpenFileEvent += OpenFile;
            iboStudioForm.RunButtonClickedEvent += RunProject;
            iboStudioForm.SaveFocusedFileEvent += SaveFocusedFile;
            iboStudioForm.SaveAllFilesEvent += SaveAllFiles;
            iboStudioForm.CutEvent += CutText;
            iboStudioForm.CopyEvent += CopyText;
            iboStudioForm.PasteEvent += PasteText;
            iboStudioForm.BuildClickedEvent += BuildProject;
            iboStudioForm.ProjectTreeCollapsed += ProjectTreeCollapsed;
            iboStudioForm.ErrorBlockCollapsed += ErrorBlockCollapsed;
            iboStudioForm.CloseProjectEvent += CloseProject;
            iboStudioForm.RemoveFileEvent += RemoveFile;
        }

        private void RemoveFile(object sender, FileArgs e)
        {
            service.DeleteFile(e.FileName);

            if (View.IsTabExist(e.FileName))
                View.RemoveTab(e.FileName);
            LoadTreeView();
        }

        private void LoadForm(object sender, EventArgs e)
        {
            View.DisableButtons(false);
        }

        private void CloseProject(object sender, EventArgs e)
        {
            View.ClearProject();
            View.DisableButtons(false);
            service.solution = null;
        }

        private void ErrorBlockCollapsed(object sender, CollapsedArgs e)
        {
            if (e.IsCollapsed == true)
                View.ShowErrorBlock();
            else
                View.HideErrorBlock();
        }

        private void ProjectTreeCollapsed(object sender, CollapsedArgs e)
        {
            if (e.IsCollapsed == true)
                View.ShowProjectTree();
            else
                View.HideProjectTree();
        }

        private void PasteText(object sender, EventArgs e)
        {
            View.PasteText();
        }

        private void CopyText(object sender, EventArgs e)
        {
            View.CopyText();
        }

        private void CutText(object sender, EventArgs e)
        {
            View.CutText();
        }

        private void SaveAllFiles(object sender, AllFilesArgs e)
        {
            foreach (var item in e.Files)
            {
                service.SaveFile(item.FileName, item.Content);
            }
        }

        private void SaveFocusedFile(object sender, FileArgs e)
        {
            foreach (var item in service.solution.Files)
            {
                if (e.FileName == item)
                    service.SaveFile(item, e.Content);
            }
        }

        private void BuildProject(object sender, AllFilesArgs e)
        {
            SaveAllFiles(sender, e);

            View.ClearErrorPage();
            View.ClearWarningPage();        
            
            if (service.FileExist("Program.cs") == false)
            {
                MessageBox.Show("Couldn't find 'Program.cs' file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CodeDomProvider code = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters param = new CompilerParameters();

            param.GenerateExecutable = true;
            param.GenerateInMemory = false;
            param.OutputAssembly = $"{service.solution.Path}\\{service.solution.FileName}.exe";
            param.TreatWarningsAsErrors = false;

            string[] fileNames = new string[service.solution.Files.Count];
            int k = 0;
            foreach (var item in service.solution.Files)
            {
                fileNames[k++] = (service.solution.Path + "\\" + item);
            }

            CompilerResults result = code.CompileAssemblyFromFile(param, fileNames);

            View.SetOutputPage(result.Output.ToString());

            if (result.Errors.Count > 0)
            {
                foreach (CompilerError item in result.Errors)
                {
                    if (item.IsWarning == true)
                    {
                        View.CreateSpaceToWarningPage();
                        View.AddToWarningPage(item.ErrorNumber, item.ErrorText, item.Line.ToString(), item.FileName);
                    }
                    else
                    {
                        View.CreateSpaceToErrorPage();
                        View.AddToErrorPage(item.ErrorNumber, item.ErrorText, item.Line.ToString(), item.FileName);
                    }
                }
                MessageBox.Show("Check Errors before running", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Build Success", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RunProject(object sender, EventArgs e)
        {
            if (File.Exists($"{service.solution.Path}/{service.solution.FileName}.exe"))
            {
                Process.Start($"{service.solution.Path}/{service.solution.FileName}.exe");
            }
            else
                MessageBox.Show("Build before Running project", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OpenFile(object sender, EventArgs e)
        {
            string fileName;
            string safeFileName;
            if(View.OpenFileDialogBlyad(out fileName, out safeFileName, false) == true)
            {
                bool isExist = false;
                foreach (var item in service.solution.Files)
                {
                    if (safeFileName == item)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist == true)
                    MessageBox.Show("That file is already exist", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    service.OpenFile(fileName, safeFileName);
                    LoadTreeView();
                }
            }                        
        }

        private void TreeViewDoubleClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            foreach (var item in service.solution.Files)
            {
                if(e.Node.Text == item)
                {
                    if(View.IsTabExist(item) == false)
                    {
                        View.AddTab(item, service.ReadFile(item));
                    }
                }
            }
        }

        private void CreateFile(object sender, EventArgs e)
        {
            NewFilePresenter presenter = IoC.Reference.Resolve<NewFilePresenter>();
            if(presenter.View.ShowDialog())
            {
                if (!presenter.FileName.Contains(".cs"))
                    presenter.FileName += ".cs";
                bool isExist = false;
                foreach (var item in service.solution.Files)
                {
                    if (presenter.FileName == item)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist == true)
                    MessageBox.Show("That file is already exist", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    service.AddFile(presenter.FileName);
                    service.WriteToFile(false);
                    LoadTreeView();
                }
            }
        }

        private void LoadTreeView()
        {
            View.LoadNodeToTreeView(service.solution.FileName, 0, false);

            foreach (var item in service.solution.Files)
            {
                View.LoadNodeToTreeView(item, 1, true);
            }
        }

        private void CreateProject(object sender, EventArgs e)
        {    
            NewProjectPresenter presenter = IoC.Reference.Resolve<NewProjectPresenter>();            
            if(presenter.View.ShowDialog() == true)
            {
                View.DisableButtons(true);
                View.ClearProject();
                service.solution = presenter.solution;
                service.WriteToFile(presenter.SubDir);
                service.AddFile("Program.cs");
                service.WriteToFile(false);
                LoadTreeView();
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void OpenProject(object sender, EventArgs e)
        { 
            string fileName;
            string safeFileName;
            if (View.OpenFileDialogBlyad(out fileName, out safeFileName, true) == true)
            {
                View.DisableButtons(true);
                View.ClearProject();
                service.LoadProject(fileName);
                LoadTreeView();
            }
        }
    }
}
