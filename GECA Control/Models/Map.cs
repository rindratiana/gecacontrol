using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GECA_Control.Models
{
    public class Map
    {
        private int mapX;
        private int mapY;
        public Coordinates[,] Matrix { get; set; }
        public int MapX
        {
            get { return mapX; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("MapX must be a positive number.");
                mapX = value;
            }
        }
        public int MapY
        {
            get { return mapY; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("MapY must be a positive number.");
                mapY = value;
            }
        }
        public Map(int x, int y)
        {
            MapX = x;
            MapY = y;
            Matrix = CreateMap();
        }
        /// <summary>
        /// Initializes the map.
        /// </summary>
        /// <returns>Returns the map's matrix.</returns
        private Coordinates[,] CreateMap()
        {
            // Pattern de la carte (peut-être externalisé dans une configuration)
            string[] pattern = {
                "$*********$**********$********",
                "***$*******B*************#****",
                "************************#*****",
                "***#**************************",
                "**$*************************#*",
                "$$***#************************",
                "**************$***************",
                "**********$*********$*****#***",
                "********************$*******$*",
                "*********#****$***************",
                "**B*********$*****************",
                "*************$$****B**********",
                "****$************************B",
                "**********************#*******",
                "***********************$***B**",
                "********$***$*****************",
                "************$*****************",
                "*********$********************",
                "*********************#********",
                "*******$**********************",
                "*#***$****************#*******",
                "****#****$****$********B******",
                "***#**$********************$**",
                "***************#**************",
                "***********$******************",
                "****B****#******B*************",
                "***$***************$*****B****",
                "**********$*********#*$*******",
                "**************#********B******",
                "s**********$*********#*B******"
            };

            Coordinates[,] matrix = new Coordinates[mapY, mapX];

            for (int y = 0; y < mapY; y++)
            {
                for (int x = 0; x < mapX; x++)
                {
                    matrix[x, y] = new Coordinates(x, y, pattern[mapY - 1 - y][x]);
                }
            }

            return matrix;
        }
        /// <summary>
        /// Print the Map
        /// </summary>
        public void PrintMap()
        {
            // Print the matrix
            for (int i = mapY-1; i >=0; i--)
            {
                for (int j = 0; j < mapX; j++)
                {
                    Console.Write(Matrix[j,i].Value);
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Update the Map
        /// </summary>
        /// <param name="caterpillar"></param>
        public void UpdateMap(Caterpillar caterpillar)
        {
            Matrix[caterpillar.Head.X, caterpillar.Head.Y].Value = caterpillar.Head.Value;
            Matrix[caterpillar.Tail.X, caterpillar.Tail.Y].Value = caterpillar.Tail.Value;
        }
    }
}
