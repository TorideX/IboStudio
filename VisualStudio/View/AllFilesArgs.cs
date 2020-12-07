using System;
using System.Collections.Generic;

namespace VisualStudio
{
    public class AllFilesArgs : EventArgs
    {
        public List<FileAndContent> Files { get; set; }
    }
    public class FileAndContent
    {
        public string FileName { get; set; }
        public string Content { get; set; }
    }
}