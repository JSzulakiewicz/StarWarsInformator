using System;
using System.IO;
using System.Net.Http;
using log4net;

namespace StarWarsInformator
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var log = LogManager.GetLogger("default");

            log.Info("Start");
            try
            {
                var client = new SwApiClient.SwApiClient(log, new HttpClient());

               // var result = client.GetPerson(1);

                //result.Wait();
            }
            catch(Exception ex)
            {
                log.ErrorFormat("An error occured: {0}. Closing.", ex.Message);
            }
            log.Info("Application closed successfully.");
        }
    }
}
