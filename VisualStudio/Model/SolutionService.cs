using System;
using System.IO;
using System.Xml.Serialization;

namespace VisualStudio.Model
{
    class SolutionService : ISolutionService
    {
        public ISolution solution { get; set; }

        public SolutionService(ISolution _solution)
        {
            solution = _solution;
        }
        
        public void WriteToFile(bool SubDir)
        {
            if (SubDir)
            {
                Directory.CreateDirectory($@"{solution.Path}\\{solution.FileName}");
                solution.Path += $@"\\{solution.FileName}";
            }

            XmlSerializer serializer = new XmlSerializer(typeof(SolutionClass));
            using (TextWriter sw = new StreamWriter($"{solution.Path}/{solution.FileName}.iSln"))
            {
                serializer.Serialize(sw, solution);
            }
        }

        public void CreateFile(string file)
        {
            if (!file.Contains(".cs"))
                file += ".cs";

            string inside = "";
            if (file == "Program.cs")
            {
                inside = "using System;\n\n" +
                "namespace MyProgram\n{\n" +
                "\tclass Program\n" +
                "\t{\n" +
                "\t\tstatic void Main()\n" +
                "\t\t{\n" +
                "\t\t\tConsole.ReadKey();\n" +
                "\t\t}\n" +
                "\t}\n" +
                "}\n";
            }
            else
            {
                inside = "using System;\n\n" +
                    "namespace MyProgram\n{\n\t\n" +
                    "}\n";
            }

            using (var fs = new FileStream($"{solution.Path}/{file}", FileMode.OpenOrCreate))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(inside);
                }
            }
        }

        public void SaveFile(string fileName, string content)
        {
            using (var fs = new FileStream($"{solution.Path}/{fileName}", FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(content);
                }
            }
        }

        public void AddFile(string fileName)
        {
            solution.Files.Add(fileName);
            CreateFile(fileName);
        }

        public void OpenFile(string path, string fileName)
        {
            string inside = File.ReadAllText(path);

            using (var fs = new FileStream($@"{solution.Path}\\{fileName}", FileMode.CreateNew))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(inside);
                }
            }
            solution.Files.Add(fileName);
            this.WriteToFile(false);
        }

        public string ReadFile(string file)
        {
            return File.ReadAllText($"{solution.Path}\\{file}");
        }

        public bool FileExist(string file)
        {
            foreach (var item in solution.Files)
            {
                if (item == file)
                    return true;
            }
            return false;
        }

        public void DeleteFile(string fileName)
        {
            solution.Files.Remove(fileName);
            WriteToFile(false);
            File.Delete($"{solution.Path}//{fileName}");
        }

        public void LoadProject(string path)
        {
            SolutionClass temp;
            XmlSerializer serializer = new XmlSerializer(typeof(SolutionClass));
            using (TextReader tr = new StreamReader(path))
            {
                temp = serializer.Deserialize(tr) as SolutionClass;
            }
            solution = temp;
        }
    }
}
