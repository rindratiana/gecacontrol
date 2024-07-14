using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GECA_Control.Services
{
    public class ApplicationService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Service started.");

            // Exemple de gestion des arguments
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                string parametre = Environment.GetCommandLineArgs()[1];
                Console.WriteLine($"Paramètre reçu depuis la console : {parametre}");

                // Modifier l'état en fonction du paramètre
                if (parametre == "start")
                {
                    Console.WriteLine("Démarrage en cours...");
                }
                else if (parametre == "stop")
                {
                    Console.WriteLine("Arrêt en cours...");
                }
                else
                {
                    Console.WriteLine("Commande non reconnue.");
                }
            }
            else
            {
                Console.WriteLine("Aucun paramètre n'a été spécifié depuis la console.");
            }

            // Attendre la fin de l'exécution (simulé ici)
            await Task.Delay(100000000);

            Console.WriteLine("Fin du service.");
        }
    }
}
