using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudio.Model
{
    interface ISolutionService
    {
        ISolution solution { get; set; }
        void WriteToFile(bool SubDir);
        void CreateFile(string file);
        void SaveFile(string fileName, string content);
        void AddFile(string fileName);
        string ReadFile(string file);
        bool FileExist(string file);
        void LoadProject(string path);
        void DeleteFile(string fileName);
        void OpenFile(string path, string fileName);
    }
}
