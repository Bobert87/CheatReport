using System;
using System.Collections.Generic;
using System.Linq;

namespace CheatReport
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Ingrese folder (ruta completa) ");
            string folder = Console.ReadLine();
            Reporter report = new Reporter(folder);
            report.GetReport();
            PrettyPrint(report);
            Console.ReadLine();
        }

        public static void PrettyPrint(Reporter r)
        {
            float copyRatio = 1f -(r.Projects.Count/r.FilesParsed);
                        
            r.Projects.OrderBy(x => x.Value.Count);
            foreach (var project in r.Projects)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(project.Key);
                int copiedProjects = 0;
                for (int i = 0; i < ((List<string>)project.Value).Count; i++)                
                {
                    if (((List<string>)project.Value).Count == 1)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else                    
                        Console.ForegroundColor = ConsoleColor.Red;                    
                    Console.WriteLine("\t " + i + ") " + project.Value[i]);
                }
            }
        }
    }
}
