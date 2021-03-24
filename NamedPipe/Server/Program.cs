using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Pipes;
using System.Web.Script.Serialization;
using CommonData;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using NamedPipeServerStream pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.InOut, 1);
            try
            {

                pipeServer.WaitForConnection();
                pipeServer.ReadMode = PipeTransmissionMode.Byte;
                using StreamReader sr = new StreamReader(pipeServer);

                string con = sr.ReadToEnd();

                Data dt = JsonConvert.DeserializeObject<Data>(con);

                Console.WriteLine(dt.name);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :  " + e.Message);
            }
        }
    }
}
