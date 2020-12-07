using System;

namespace VisualStudio
{
    public class FileArgs : EventArgs
    {
        public string FileName { get; set; }
        public string Content { get; set; }
    }
}