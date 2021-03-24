using System;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Web.Script.Serialization;
using CommonData;
using Newtonsoft.Json;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Data cd = new Data();
            cd.index = 0;
            cd.name = "Client";
            try
            {
                using NamedPipeClientStream pipeClient = new NamedPipeClientStream("localhost", "testpipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None);
                pipeClient.Connect();
                using StreamWriter sw = new StreamWriter(pipeClient);

                string str = JsonConvert.SerializeObject(cd);

                Console.WriteLine("---------------------------------------------");

                sw.Write(str);
                Console.WriteLine(cd.index.ToString());
                Console.WriteLine(cd.name);

                Console.WriteLine("---------------------------------------------");

                sw.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  " + e.Message);
            }
        }
    }
}
