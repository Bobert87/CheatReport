using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CheatReport
{
    
    public class Reporter
    {
        public Reporter(string folder)
        {
            Folder = folder;
            Projects = new Dictionary<string, List<string>>();
        }

        private string Folder{ get; set; }
        private const string VSEXTENSION = "csproj";
        public Dictionary<string, List<string>> Projects;
        public int FilesParsed { get; set; }
        private void AddProject(string guid, string proj)
        {
            if (!Projects.ContainsKey(guid))
            {
                Projects.Add(guid,new List<string>() {proj});
            }
            else
            {
                Projects[guid].Add(proj);
            }
        }

        private List<string> LookForProjects(string currentFolder)
        {
            DirectoryInfo di = new DirectoryInfo(currentFolder);
            FileInfo[] projectFilesInFolder = di.GetFiles("*." + VSEXTENSION, SearchOption.AllDirectories);
            List<string> files = projectFilesInFolder.Select(fileInfo => fileInfo.FullName).ToList();            
            return files;
        }

        private string GetGuid(string csProjFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(csProjFile);            
            return xmlDoc.GetElementsByTagName("ProjectGuid")[0].InnerText.Replace("{","").Replace("}","");
        }

        public void GetReport()
        {
            List<string> projectList = LookForProjects(Folder);
            FilesParsed = projectList.Count;
            foreach (var p in projectList)
            {                
                AddProject(GetGuid(p),p);
            }
        }
    }
}
