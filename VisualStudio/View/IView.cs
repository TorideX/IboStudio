using System;

namespace VisualStudio.View
{
    interface IView
    {
        bool ShowDialog();
        void CloseDialog(bool isOK);
    }
}
