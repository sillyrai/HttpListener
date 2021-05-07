using System.Net;
using System.Threading;

namespace Listener
{
    public delegate void Request(string Recieved);
    public class HttpListener
    {
        private static string OldResponse = null;
        private static bool Listening = false;
        private static WebClient web = new WebClient();

        public event Request Recieved;
        public void StartListener(string URL)
        {
            Listening = true;
            new Thread(() =>
            {
                while (Listening)
                {
                    Thread.Sleep(1000);
                    string Response = web.DownloadString(URL);
                    if (Response != OldResponse)
                    {
                        Recieved?.Invoke(Response);
                        OldResponse = Response;
                    }
                }
            }).Start();
        }
        public void StopListener()
        {
            Listening = false;
        }
    }
}
