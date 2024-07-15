using GECA_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GECA_Control.Services
{
    public class CaterpillarControlService
    {
        /// <summary>
        /// Moves the caterpillar to a new position.
        /// </summary>
        /// <param name="caterpillar">The caterpillar to be moved.</param>
        /// <returns>The caterpillar with an updated position.</returns>
        public static void MoveCaterpillar(Caterpillar caterpillar,Map map,DoMove move,string path)
        {
            caterpillar.Move(move);
            FileService.WriteFile(move, path);
            //Update the map based on the caterpillar's position.
            Map.UpdateMap(caterpillar);

            //Print the map on the console
            //map.PrintMap();
            //return caterpillar;
        }
    }
}
