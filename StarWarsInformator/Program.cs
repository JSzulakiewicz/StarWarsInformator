using System;
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
                log.Info("Not enough parameters! Proper invocation should be: StarWarsInformator.exe <source uri> <target file>, in example: StarWarsInformator.exe http://swapi.dev/api/people/1/ Skywalker.json");
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
