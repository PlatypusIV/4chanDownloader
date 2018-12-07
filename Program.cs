using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace downloadFourChanImages
{
    class Program
    {
        static void Main(string[] args)
        {
            bool again = false;

            do
            {
                Console.WriteLine("Please enter a desired name for the folder:");
                string folderName = Console.ReadLine();
                Console.WriteLine("enter url:");
                string url = Console.ReadLine();

                Console.WriteLine("Downloading...");

                Classes.GetHtml getHtml = new Classes.GetHtml();
                string htmlSource = getHtml.getPageHtml(url);
                Dictionary<string, string> namesAndLinks = getHtml.getImageLinks(htmlSource);

                Classes.DownloadImages download = new Classes.DownloadImages(folderName, namesAndLinks);

                Console.WriteLine("Download complete! Would you like to go again: y/n");

                string yesNo = Console.ReadLine();

                if (yesNo.ToLower().Equals("y"))
                {
                    again = true;
                }
                else
                {
                    again = false;
                }

            } while (again);

            //foreach (KeyValuePair<string, string> kv in namesAndLinks)
            //{
            //    Console.WriteLine($"{kv.Key}:{kv.Value}");
            //}

            Console.ReadLine();           
        }

        
    }
}
