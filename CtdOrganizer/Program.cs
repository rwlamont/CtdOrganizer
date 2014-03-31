using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace CtdOrganizer
{
    class Program
    {
        static List<string> siteNames = new List<string>();
        static void Main(string[] args)
        {

            Directory.CreateDirectory("Unknown");
            bool notEnd = true;
            using (var reader = new StreamReader("input.txt"))
            {
                while (notEnd)
                {
                    string str = reader.ReadLine();
                    if (str == null)
                    {
                        notEnd = false;
                        break;
                    }
                    siteNames.Add(str);
                    Directory.CreateDirectory(str);
                }
            }

            
            ProcessDirectory("C:\\Users\\rwlamont\\Documents\\CNV");

            
            Console.Read();


        }

        // Process all files in the directory passed in, recurse on any directories  
        // that are found, and process the files they contain. 
        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);


        }

        // Insert logic for processing found files here. 
        public static void ProcessFile(string path)
        {
           
            var fileNameOnly = Path.GetFileNameWithoutExtension(path);
            fileNameOnly.Remove(0, 10);
            fileNameOnly.Remove(fileNameOnly.Length - 5, 4);
            if (fileNameOnly.Contains("Unknown") || fileNameOnly.Contains("unknown"))
            {
                File.Move(path, Directory.GetCurrentDirectory() + "\\Unknown\\" + Path.GetFileName(path));
            }
            else
            {

                string[] splits = fileNameOnly.Split('_');
                for (int i = 0; i < splits.Length; i++)
                {
                    if (siteNames.Contains(splits[i]))
                    {
                        string newPath = Directory.GetCurrentDirectory() + "\\" + splits[i] + "\\" + Path.GetFileName(path);
                        File.Move(path, newPath);
                    }

                }
            }
        }
    }
}
