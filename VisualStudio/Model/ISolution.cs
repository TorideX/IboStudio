using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudio.Model
{
    interface ISolution
    {
        string FileName { get; set; }
        string Path { get; set; }
        List<string> Files { get; set; }
    }
}
