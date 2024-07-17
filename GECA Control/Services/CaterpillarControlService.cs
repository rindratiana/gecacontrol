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
            caterpillar.Move(move,false);
            int k = 0;
            Coordinates tempHead = caterpillar.Tail;
            foreach (var intermediate in caterpillar.Intermediate)
            {
                Caterpillar catTemp = new Caterpillar
                {
                    Head = tempHead,
                    Tail = intermediate,
                };
                catTemp.Move(move,true);
                caterpillar.Intermediate[k].X = catTemp.Tail.X;
                caterpillar.Intermediate[k].Y = catTemp.Tail.Y;
                tempHead = catTemp.Tail;
                k++;
            }
            FileService.WriteFile(move, path);
        }
    }
}
