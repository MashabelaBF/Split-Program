using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Split.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "";
            string outputFolder = "";
            string maxSize = "0";


            Dictionary<string, string> inputArgs = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i = i + 2)
            {

                inputArgs.Add(args[i], args[i + 1]);

                //  Console.WriteLine(arg);
            }

            inputArgs.TryGetValue("-inputfile", out inputFile);
            inputArgs.TryGetValue("-outputfolder", out outputFolder);
            inputArgs.TryGetValue("-maxsize", out maxSize);

            Split.Library.Splitter splitterLib = new Library.Splitter();

            try
            {

                //check to see if the output folder exists
                if (!splitterLib.CheckFileExists(inputFile))
                {
                    throw new Exception("Input File does not exist.");

                }

                if (!splitterLib.CheckFolderExists(outputFolder))
                {
                    //check to see if the inputfile exists
                    throw new Exception("output Folder does not exist.");
                }
                //split the file

                List<string> splitFiles = new List<string>();

                string baseFileName = System.IO.Path.GetFileNameWithoutExtension(inputFile);


                splitFiles = splitterLib.SplitFile(inputFile, outputFolder, baseFileName, int.Parse(maxSize));

                Console.WriteLine("File " + inputFile + " split into: ");
                foreach (string fileName in splitFiles)
                {
                    Console.WriteLine("fileName : " +  fileName + Environment.NewLine);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block and exiting.");
            }
            //keep the console running
            Console.ReadLine();


        }
    }
}
