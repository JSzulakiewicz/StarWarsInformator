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
            if (args.Length < 2)
            {
                log.Info("Not enough parameters!");
            }
                
            try
            {
                
                var client = new SwApiClient.SwApiClient(log, new HttpClient());

                var result = new TargetPerson(client, args[0]);

                result.ExportToJson(args[1]);
            }
            catch(Exception ex)
            {
                log.ErrorFormat("An error occured: {0}. Closing.", ex.Message);
            }
            log.Info("Application closed successfully.");
        }
    }
}
