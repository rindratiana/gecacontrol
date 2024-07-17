using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
            Coordinates headTemp = new Coordinates
            {
                X = Head.X,
                Y = Head.Y
            };
            int i = 0;
            foreach(var coordinates in Intermediate)
            {
                Caterpillar temp = new Caterpillar
                {
                    Head = headTemp,
                    Tail = coordinates
                };
                temp.MoveIntermediate(move,i,Intermediate,Tail);
                headTemp = temp.Tail;
                i++;
            }
        }
        public void MoveIntermediate(DoMove move,int indice,List<Coordinates> intermediate,Coordinates tail)
        {
            switch (move.Key.ToUpper())
            {
                case "R":
                    for (int i = 0; i < move.Value; i++)
                    {
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            intermediate[i].X = Head.X-1;
                            intermediate[i].Y = Head.Y;
                            /*tail.X = intermediate[i].X - 1;
                            tail.Y = intermediate[i].Y;*/
                        }
                        ControlArea();
                    }
                    /*Coordinates lastSegment = intermediate[intermediate.Count-1];
                    Caterpillar cTemp = new Caterpillar
                    {
                        Head = lastSegment,
                        Tail = tail
                    };
                    if(cTemp.DistanceHeadTail() > 1 && !cTemp.IsDiagonal()){
                        tail.X = cTemp.Head.X-1;
                        tail.Y = cTemp.Tail.Y;
                    }
                    else
                    {
                        tail.X = cTemp.Head.X;
                        tail.Y = cTemp.Head.Y - 1;
                    }*/
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
        public void Move(DoMove move,bool forIntermediate)
        {
            switch (move.Key.ToUpper())
            {
                case "R":
                    for(int i = 0; i < move.Value; i++)
                    {
                        if(!forIntermediate) Head.X++;
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            if (forIntermediate && Math.Abs(Head.Y - Tail.Y) >= 2)
                            {
                                if (Head.Y > Tail.Y) { Tail.Y++; Tail.X = Head.X - 1; }
                                else
                                {
                                    Tail.Y--;
                                    Tail.X = Head.X + 1;
                                }
                            }
                            else
                            {
                                Tail.Y = Head.Y;
                                Tail.X = Head.X - 1;
                            }
                        }
                        ControlArea();
                    }
                    break;
                case "L":
                    for (int i = 0; i < move.Value; i++)
                    {
                        if (!forIntermediate) Head.X--;
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            if (forIntermediate && Math.Abs(Head.Y - Tail.Y) >=2 )
                            {
                                if (Head.Y > Tail.Y) { Tail.Y++; Tail.X = Head.X + 1; }
                                else { Tail.Y--; Tail.X = Head.X - 1; }
                            }
                            else
                            {
                                Tail.X = Head.X + 1;
                                Tail.Y = Head.Y;
                            }
                        }
                        ControlArea();
                    }
                    break;
                case "U":
                    for(int i = 0; i < move.Value; i++)
                    {
                        if (!forIntermediate) Head.Y++;
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            if (forIntermediate && Math.Abs(Head.X - Tail.X)  >= 2)
                            {
                                if (Head.X > Tail.X)
                                {
                                    Tail.X++;
                                }
                                else { 
                                    Tail.X--; 
                                }
                            }
                            else
                            {
                                Tail.X = Head.X;
                                Tail.Y = Head.Y - 1;
                            }
                        }
                        ControlArea();
                    }
                    break;
                
                case "D":
                    for (int i = 0; i < move.Value; i++)
                    {
                        if (!forIntermediate) Head.Y--;
                        Map.ControlObstacle(Head);
                        if (DistanceHeadTail() > 1 && !IsDiagonal())
                        {
                            if (forIntermediate && Math.Abs(Head.X - Tail.X) >= 2)
                            {
                                if (Head.X > Tail.X) Tail.X++;
                                else Tail.X--;
                            }
                            else { 
                                Tail.X = Head.X;
                                Tail.Y = Head.Y + 1;
                            }
                        }
                        ControlArea();
                    }
                    break;
                default:
                    break;
            }
            /*if (Intermediate.Count > 0) { 
                ModificationIntermediate(move);
            }*/
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

        public bool IsDiagonal(Coordinates head,Coordinates tail)
        {
            bool response = false;
            if ((head.X + 1 == tail.X && (head.Y - 1 == tail.Y || head.Y + 1 == tail.Y)) /*Contol Up and Down left*/
                || (head.X - 1 == tail.X && (head.Y - 1 == tail.Y || head.Y + 1 == Tail.Y))) response = true; /*Contol Up and Down right*/
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
        /// Update the data in the datamatrix
        /// </summary>
        public void UpdateMatrix()
        {
            for (int i = 0; i < Intermediate.Count; i++)
            {
                if (Intermediate[i].Value == 'T') Intermediate[i].Value = '0';
            }
        }
        /// <summary>
        /// Grow the caterpillar when hit a booster a update the size
        /// </summary>
        /// <param name="nbrSegments">Nbr of segments</param>
        public void GrowCaterpillar(int nbrSegments)
        {

            //Coordinates currentTail = Tail;
            Coordinates temp = new Coordinates();
            temp.Value = '0';
            /*for (int i = 0; i < nbrSegments; i++)
            {*/
            Coordinates head = new Coordinates();
            Coordinates tail = new Coordinates();
            if (Intermediate.Count > 1)
            {
                head = Intermediate[Intermediate.Count-2];
                tail = Intermediate[Intermediate.Count-1];
            }
            else
            {
                tail = Tail;
                if (Intermediate.Count == 1) head = Intermediate[Intermediate.Count - 1];
                else head = Head;
            }
            if (head.X == tail.X) // Vertical
            {
                if (Head.Y > Tail.Y)
                {
                    temp.Y = tail.Y - 1;
                }
                else
                {
                    temp.Y = tail.Y + 1;
                }
                temp.X = tail.X;
            }
            else if (head.Y == tail.Y) // Horizontal
            {
                if (Head.X > Tail.X)
                {
                    temp.X = tail.X - 1;
                }
                else
                {
                    temp.X = tail.X + 1;
                }
                temp.Y = tail.Y;
            }
            Intermediate.Add(temp);
            /*}*/
        }
    }
}
