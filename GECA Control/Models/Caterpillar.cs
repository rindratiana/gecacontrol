using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GECA_Control.Models
{
    public class Caterpillar
    {
        public Coordinates Head { get; set; }
        public Coordinates Tail { get; set; }
        public List<Coordinates> Intermediate { get; set; }
        public int Size { get; set; }
        public static string controlObstacle;
        public Caterpillar()
        {
            InitializeCaterpillar();
        }
        public Caterpillar(Coordinates head, Coordinates tail)
        {
            Head = head;
            Tail = tail;
        }

        /// <summary>
        /// Initialization of the caterpillar
        /// </summary>
        private void InitializeCaterpillar()
        {
            //TODO prendre les valeurs
            Tail = new Coordinates(0, 0, 'T');
            Head = new Coordinates(0, 0,'H');
            Intermediate = new List<Coordinates>();
            Size = 1;
        }
        public void ModificationIntermediate(DoMove move)
        {
            Coordinates currentHead = Head;
            foreach(var coordinates in Intermediate)
            {
                /*Coordinates coordinateTemp = Intermediate.Where(c => c.X == currentHead.Y || Math.Abs(Head.Y - c.X) == 1).First();*/
                Caterpillar temp = new Caterpillar
                {
                    Head = currentHead, Tail = coordinates 
                };
                temp.MoveIntermediate(move);
                currentHead = temp.Tail;
            }
            /*Tail = Intermediate[Intermediate.Count - 1];
            Tail.Value = 'T';
            Head.Value = 'H';*/
        }
        public void MoveIntermediate(DoMove move)
        {
            switch (move.Key.ToUpper())
            {
                case "R":
                    for (int i = 0; i < move.Value; i++)
                    {
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            Head.X++;
                            Tail.Y = Head.Y;
                            Tail.X = Head.X - 1;
                        }
                        ControlArea();
                    }
                    break;
                case "U":
                    for (int i = 0; i < move.Value; i++)
                    {
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            Head.Y++;
                            Tail.X = Head.X;
                            Tail.Y = Head.Y - 1;
                        }
                        ControlArea();
                    }
                    break;
                case "L":
                    for (int i = 0; i < move.Value; i++)
                    {
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            Head.X--;
                            Tail.X = Head.X + 1;
                            Tail.Y = Head.Y;
                        }
                        ControlArea();
                    }
                    break;
                case "D":
                    for (int i = 0; i < move.Value; i++)
                    {
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            Head.Y--;
                            Tail.X = Head.X;
                            Tail.Y = Head.Y + 1;
                        }
                        ControlArea();
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Moves the Caterpillar based on the given instruction.
        /// </summary>
        /// <param name="move">The movement instruction from the Rider.</param>
        public void Move(DoMove move)
        {
            switch (move.Key.ToUpper())
            {
                case "R":
                    for(int i = 0; i < move.Value; i++)
                    {
                        Head.X++;
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            Tail.Y = Head.Y;
                            Tail.X = Head.X - 1;
                        }
                        ControlArea();
                    }
                    break;
                case "U":
                    for(int i = 0; i < move.Value; i++)
                    {
                        Head.Y++;
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            Tail.X = Head.X;
                            Tail.Y = Head.Y - 1;
                        }
                        ControlArea();
                    }
                    break;
                case "L":
                    for (int i = 0; i < move.Value; i++)
                    {
                        Head.X--;
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            Tail.X = Head.X + 1;
                            Tail.Y = Head.Y;
                        }
                        ControlArea();
                    }
                    break;
                case "D":
                    for (int i = 0; i < move.Value; i++)
                    {
                        Head.Y--;
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            Tail.X = Head.X;
                            Tail.Y = Head.Y + 1;
                        }
                        ControlArea();
                    }
                    break;
                default:
                    break;
            }
            if (Intermediate.Count > 0) { 
                ModificationIntermediate(move);
            }
        }
        /// <summary>
        /// Control if the caterpillar crosses the zone on the map. 
        /// </summary>
        public void ControlArea()
        {
            if (Head.X >= 30) Head.X = 0;
            else if (Head.Y >= 30) Head.Y = 0;
            else if (Head.X < 0) Head.X = 29;
            else if (Head.Y < 0) Head.Y = 29;
        }
        /// <summary>
        /// Checks if the tail is positioned diagonally relative to the head.
        /// </summary>
        /// <returns>True if the tail is diagonal to the head; otherwise, false.</returns>
        public bool IsDiagonal()
        {
            bool response = false;
            if ((Head.X + 1 == Tail.X && (Head.Y - 1 == Tail.Y || Head.Y + 1 == Tail.Y)) /*Contol Up and Down left*/
                || (Head.X - 1 == Tail.X && (Head.Y-1 == Tail.Y || Head.Y+1 == Tail.Y))) response = true; /*Contol Up and Down right*/
            return response;
        }
        /// <summary>
        /// Calculate the Euclidean distance between the head and the tail
        /// </summary>
        /// <returns>The distance</returns>
        public double DistanceHeadTail()
        {
            int deltaX = Head.X - Tail.X;
            int deltaY = Head.Y - Tail.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        /// <summary>
        /// Grow the caterpillar when hit a booster a update the size
        /// </summary>
        /// <param name="nbrSegments">Nbr of segments</param>
        public void GrowCaterpillar(int nbrSegments)
        {

            Coordinates currentTail = Tail;
            for (int i = 0; i < nbrSegments; i++)
            {
                Coordinates newSegmentCoordinates = new Coordinates(currentTail.X, currentTail.Y,'0');
                Intermediate.Add(newSegmentCoordinates);

                if (Head.X == Tail.X) // Vertical
                {
                    if (Head.Y > Tail.Y)
                    {
                        currentTail.Y--;
                    }
                    else
                    {
                        currentTail.Y++;
                    }
                }
                else if (Head.Y == Tail.Y) // Horizontal
                {
                    if (Head.X > Tail.X)
                    {
                        currentTail.X--;
                    }
                    else
                    {
                        currentTail.X++;
                    }
                }
            }
            Tail = currentTail; // Update Tail
        }
    }
}
