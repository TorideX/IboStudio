using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace VisualStudio.Model
{
    public class SolutionClass : ISolution
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public List<string> Files { get; set; }

        public SolutionClass()
        {
            Files = new List<string>();
        }
    }
}
