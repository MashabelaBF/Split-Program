using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


namespace Split.Library
{
    /// <summary>
    /// This is a splitter class for splitting files
    /// </summary>
    public class Splitter
    {
        /// <summary>
        /// Checks to see if the max size is valid.
        /// </summary>
        /// <param name="enteredMaxSize"></param>
        /// <returns></returns>
        public  int CheckMaxSize(int enteredMaxSize)
        {
            // int maxSize = 0;
            int returnValue = 0;

            try
            {
               // Console.Write("Please enter the size of each split file: ");
                //maxSize = Convert.ToInt32(Console.ReadLine());

                //Console.WriteLine(maxSize.GetType() != typeof(int));
                //Console.WriteLine(maxSize);


                if ( (enteredMaxSize <= 0))
                {
                    // Console.WriteLine("Value cannot be less than zero");
                    //Console.WriteLine(maxSize);
                    // CheckMaxSize();

                    throw new Exception("Value cannot be less than zero");

                }
                else
                {
                    returnValue = enteredMaxSize;
                    //CheckMaxSize();
                    //Console.WriteLine("else");
                    //Console.WriteLine(maxSize);
                }

            }
            catch (Exception e)
            {
                throw e;

              //  Console.WriteLine(e.Message);
              //  CheckMaxSize();
            }
            return returnValue;
        }

        /// <summary>
        /// Checks to see if the input file exists.
        /// </summary>
        /// <param name="checkFile"></param>
        /// <returns></returns>
        public  bool CheckFileExists(string checkFile)
        {
            //string path;

           // Console.Write("Please enter the file path: ");
        //path = Console.ReadLine();
        try
            { 

            if (!File.Exists(checkFile) )
            {
                // Try to create the directory.
                // Console.WriteLine("Path does not exist");
                // CheckPath();
                return false;
            }
            else
            {

                    //Console.WriteLine("File Found ");
                    return true;

            }

            }
         catch(Exception ex)
         {
                throw ex;

         }

        }

        /// <summary>
        /// Check to see if the folder exists and create if necessary.
        /// </summary>
        /// <param name="outputFolder"></param>
        /// <returns></returns>
        public  bool CheckFolderExists(string outputFolder)
        {
            //string savePath = "";

            bool retValue = false;
            try
            {
               // Console.Write("Please enter the path you want to save the file to: ");
               // savePath = Console.ReadLine();

                if (!Directory.Exists(NormalizeFilePath(outputFolder)) )
                {
                    // Try to create the directory.
                    // Console.WriteLine("Path does not exist");
                    // SavePath();
                    System.IO.Directory.CreateDirectory(outputFolder);

                    retValue =  true;

                }
                else
                {
                    retValue = true;
                }
               
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                throw e;

            }
            return retValue;
        }

        /// <summary>
        /// Normalizes the file path to ensure that it has a directory separator character at the end
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string NormalizeFilePath(string filePath)
        {
            if (!filePath.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
                {
                filePath += System.IO.Path.DirectorySeparatorChar.ToString();

                }

            
        return filePath;

        }

        /// <summary>
        /// Splits the input file into the blocks as required
        /// </summary>
        /// <param name="inFileName"></param>
        /// <param name="savePath">Where the split files need to be saved</param>
        /// <param name="maxSize">The max size of the file</param>
        /// <returns></returns>
        public List<string> SplitFile(string inFileName, string savePath, string baseOutputFileName, int maxSize)
        {
            int recordCounter=0;
            int currentFileSize = 0;
            string line = "";
            int fileCounter = 1;

            List<string> retVal = new List<string>();

            string fileName = NormalizeFilePath(savePath) + baseOutputFileName + "_split_" + fileCounter.ToString() + ".DSP";
            string logName = NormalizeFilePath(savePath) + baseOutputFileName + "_log_" + fileCounter.ToString() + ".txt";

            StreamWriter OurStream = File.CreateText(fileName);
            StreamWriter log_Name = File.CreateText(logName);
            retVal.Add(fileName);


            //path of th3 original file - D:/Users/Bran.mashabela/Desktop/projects/dsp/#121.DSP

            //Pass the file path and file name to the StreamReader constructor
            using (StreamReader file = new StreamReader(inFileName))
            {

             

                while (!file.EndOfStream)
                {
                    line = file.ReadLine();
                    currentFileSize += line.Length;
                    
                    OurStream.WriteLine(line);


                    try
                    {
                        if(line.Contains("MOVED TO PURGE LIST")){
                           recordCounter++; 
                        }
                        
                        if ( currentFileSize >= maxSize && line.Contains("MOVED TO PURGE LIST")) //we need to check to see if the max file size reached or line contains end
                        {
                            log_Name.WriteLine("Records found "+recordCounter+"");
                            log_Name.Close();
                            //we need to create a new file 
                            OurStream.Close();
                            Console.WriteLine(baseOutputFileName + "_"+ fileCounter+ " created succesfuly");
                            currentFileSize = 0;
                            fileCounter++;

                            fileName = NormalizeFilePath(savePath) + baseOutputFileName + "_split_" + fileCounter.ToString() + ".DSP";
                            logName = NormalizeFilePath(savePath) + baseOutputFileName + "_log_" + fileCounter.ToString() + ".txt";

                            retVal.Add(fileName);

                            log_Name= File.CreateText(logName);
                            OurStream = File.CreateText(fileName); 
                            recordCounter = 0;
                        }
                       
                       // StreamWriter OurStream = File.CreateText(NormalizeFilePath(savePath) + baseOutputFileName + "_split_" + fileCounter.ToString() + ".DSP");



                    }
                   
                    catch (Exception readEx)
                    {
                        throw readEx;
                    }

                }
                Console.WriteLine("records found "+recordCounter);
                Console.WriteLine(" File Split Complete! ");

            }


            return retVal;



            //////////    //Read the first line of text
             
          

            //////////          //create a file
            //////////StreamWriter OurStream = File.CreateText(NormalizeFilePath(savePath) + baseOutputFileName + "_split_" + fileCounter.ToString() + ".DSP");
            ////////////Console.WriteLine("Spliting Files...");
            ////////////Continue to read until you reach end of file
            //////////while (line != null)
            //////////{
            //////////    //write line to a file
            //////////    OurStream.WriteLine(line);

            //////////    //get the size of the file
            //////////    currentFileSize += line.Length;

            //////////    //create a new file when it hits the file size and its end of file
            //////////    if (currentFileSize >= maxSize && line.Contains("MOVED TO PURGE LIST"))
            //////////    {
            //////////        OurStream.Close();
            //////////        // Console.WriteLine("File " + fileCounter.ToString() + " Created succesfuly");

            //////////        currentFileSize = 0;
            //////////        fileCounter += 1;
            //////////        OurStream = File.CreateText(savePath + "/Splitfile" + count + ".DSP");
            //////////    }
            //////////    //catch exception thrown at the end of file
            //////////    try
            //////////    {
            //////////        //Read the next line
            //////////        line = file.ReadLine();
            //////////    }
            //////////    catch (Exception)
            //////////    {
            //////////        Console.WriteLine("file finished");
            //////////    }
            //////////}
            //////////OurStream.Close();
            //////////Console.WriteLine("Spliting files Complete");

            ////////////close the file
            //////////file.Close();
        }
    }
}
