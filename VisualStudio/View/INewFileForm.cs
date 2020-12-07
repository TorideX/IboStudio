using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudio.View
{
    interface INewFileForm
    {
        event EventHandler CancelButtonClicked;
        event EventHandler<FileNameArgs> OKButtonClicked;
        void CloseForm();
    }
}
