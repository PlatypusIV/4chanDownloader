using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace downloadFourChanImages.Classes
{
    class GetHtml
    {

        public string getPageHtml(string url)
        {
            string source = "";
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.UseDefaultCredentials = true;
                req.UserAgent = "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.5; ko; rv:1.9.1b2) Gecko/20081201 Firefox/3.1b2";       

                using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    source = reader.ReadToEnd();
                }                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return source;
        }

        public Dictionary<string,string> getImageLinks(string html)
        {
            
            Dictionary<string, string> imageNamesAndLinks = new Dictionary<string, string>();           
            try
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='fileText']/a");

                foreach(var node in nodes)
                {
                    string realUrl = $"https:{node.Attributes["href"].Value}";
                    imageNamesAndLinks.Add(realUrl, node.InnerText);
                }


            }catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return imageNamesAndLinks;
        }
    }
}
