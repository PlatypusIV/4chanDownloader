using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace downloadFourChanImages.Classes
{
    class DownloadImages
    {
        Dictionary<string, string> imagesToDownload;
        string fullPath;

        public DownloadImages(string folderName, Dictionary<string, string> imagesAndNames)
        {
            imagesToDownload = imagesAndNames;
            fullPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\{folderName}";

            CreateDirectoryIfNeeded(fullPath);
            DownloadAndSave();
        }

        private void DownloadAndSave()
        {
            
            try
            {
                
                int counter = 0;

                foreach(var keyValuePair in imagesToDownload)
                {
                    using(var client = new WebClient())
                    {
                        byte[] data = client.DownloadData(keyValuePair.Key);
                        using(var memoryStream = new MemoryStream(data))
                        {
                            string fileName = $"{keyValuePair.Value}";
                            try
                            {
                                using (var fileStream = new FileStream($"{fullPath}\\{fileName}", FileMode.CreateNew))
                                {
                                    memoryStream.CopyTo(fileStream);
                                }
                            }
                            catch(IOException exc)
                            {
                                int numberToTackOn = new Random().Next(0, 9999999);
                                fileName = $"({numberToTackOn}){fileName}";
                                using (var fileStream = new FileStream($"{fullPath}\\{fileName}", FileMode.CreateNew))
                                {
                                    memoryStream.CopyTo(fileStream);
                                }
                            }                            
                        }
                    }
                    counter++;
                    if (counter == 5)
                    {
                        Thread.Sleep(1000);
                        counter = 0;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void CreateDirectoryIfNeeded(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
