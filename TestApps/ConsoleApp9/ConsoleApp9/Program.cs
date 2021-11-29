using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApp9
{
    class Program
    {
        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForSite())
            {
                using (IsolatedStorageFileStream isfs = isf.OpenFile("CookieExCookies",
                    FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(isfs))
                    {
                        foreach (Cookie cookieValue in response.Cookies)
                        {
                            sw.WriteLine("Cookie: " + cookieValue.ToString());
                        }
                        sw.Close();
                    }
                }

            }
        }

        static void Main(string[] args)
        {
            CookieContainer cookieJar = new CookieContainer();

            var request = (HttpWebRequest)WebRequest.Create("https://tecumgamevalencia.com/#reservar");
            request.CookieContainer = cookieJar;
            var response = (HttpWebResponse)request.GetResponse();
            var aa = response.Cookies["PHPSESSID"];

            Console.ReadLine();
        }
    }
}
