using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GECA_Control.Models
{
    public class Map
    {
        private int mapX;
        private int mapY;
        public static Coordinates[,] Matrix { get; set; }
        public Map() { }
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
        [JsonConstructor]
        public Map(int mapX, int mapY)
        {
            MapX = mapX;
            MapY = mapY;
            if (Matrix == null)
            {
                Matrix = CreateMap();
            }
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
        public static void UpdateMap(Caterpillar caterpillar)
        {
            Matrix[caterpillar.Head.X, caterpillar.Head.Y].Value = caterpillar.Head.Value;
            Matrix[caterpillar.Tail.X, caterpillar.Tail.Y].Value = caterpillar.Tail.Value;
        }

        /// <summary>
        /// Checks if the head of the caterpillar overlaps with the spice.
        /// </summary>
        /// <param name="x">The x-coordinate of the caterpillar</param>
        /// <param name="y">The y-coordinate of the caterpillar</param>
        public static void ControlObstacle(Coordinates coordinates)
        {
            if(coordinates.X < 30 && coordinates.Y < 30 && coordinates.X >= 0 && coordinates.Y >= 0) { 
                if (Map.Matrix[coordinates.X, coordinates.Y].Value == '$')
                {
                    Map.Matrix[coordinates.X, coordinates.Y].Value = '*';
                    Caterpillar.controlObstacle = "spice";
                }
                else if(Map.Matrix[coordinates.X, coordinates.Y].Value == 'B')
                {
                    Map.Matrix[coordinates.X, coordinates.Y].Value = '*';
                    Caterpillar.controlObstacle = "booster";
                }
                else if (Map.Matrix[coordinates.X, coordinates.Y].Value == '#')
                {
                    Map.Matrix[coordinates.X, coordinates.Y].Value = '*';
                    Caterpillar.controlObstacle = "obstacles";
                }
            }
        }
    }
}
