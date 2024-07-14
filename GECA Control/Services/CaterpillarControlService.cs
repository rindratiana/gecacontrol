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
        public static Caterpillar MoveCaterpillar(Caterpillar caterpillar,Map map, List<DoMove> movements,string path)
        {
            //Do the movement
            foreach (DoMove move in movements)
            {
                caterpillar.Move(move);
                FileService.WriteFile(move,path);
            }

            //Update the map based on the caterpillar's position.
            map.UpdateMap(caterpillar);

            //Print the map on the console
            map.PrintMap();
            return caterpillar;
        }
    }
}
