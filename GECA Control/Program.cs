// See https://aka.ms/new-console-template for more information
using GECA_Control.Models;
using GECA_Control.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Créer le host builder
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Ajouter le service d'application
                services.AddHostedService<ApplicationService>();
            });

        // Construire et démarrer le host
        await builder.RunConsoleAsync();

        /*var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        var pathCommandLog = config["PathLogCommand"];

        Map map = new Map(30, 30);
        List<DoMove> movements = new List<DoMove>
        {
            new DoMove("R", 4),
            new DoMove("U", 4),
            new DoMove("L", 3),
            new DoMove("D", 1),
            new DoMove("R", 4),
            new DoMove("D", 1),
            new DoMove("L", 5),
            new DoMove("R", 2)
        };
        Caterpillar caterpillar = new Caterpillar();
        CaterpillarControlService.MoveCaterpillar(caterpillar, map, movements, pathCommandLog);*/
    }
}
