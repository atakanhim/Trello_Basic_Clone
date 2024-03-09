using Microsoft.Extensions.Configuration;

namespace trelloClone.Persistence
{
    public class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/trelloClone.MVC"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("MsSQL");
            }
        }
    }
}
