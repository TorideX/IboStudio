using System;
using System.Windows.Forms;
using VisualStudio.Model;
using VisualStudio.Presenter;
using VisualStudio.View;

namespace VisualStudio
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IoC.Reference.Register<IboStudioForm, IIboStudioForm>().
                Register<NewProjectForm, INewProjectForm>().
                Register<NewFileForm,INewFileForm>().
                Register<SolutionClass,ISolution>().
                Register<SolutionService,ISolutionService>().
                Register<NewFilePresenter>().
                Register<NewProjectPresenter>().
                Register<IboStudioPresenter>().Build();

            IboStudioPresenter iboStudio = IoC.Reference.Resolve<IboStudioPresenter>();

            Application.Run(iboStudio.View as IboStudioForm);
        }
    }
}
