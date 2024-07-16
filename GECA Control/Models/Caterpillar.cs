﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GECA_Control.Models
{
    public class Caterpillar
    {
        public Coordinates Head { get; set; }
        public Coordinates Tail { get; set; }
        public List<Coordinates> IntermediateSegments { get; set; }
        public int Size { get; set; }
        public Caterpillar()
        {
            InitializeCaterpillar();
        }
        /// <summary>
        /// Initialization of the caterpillar
        /// </summary>
        private void InitializeCaterpillar()
        {
            //TODO prendre les valeurs
            Tail = new Coordinates(0, 0, 'T');
            Head = new Coordinates(0, 0,'H');
            Size = 1;
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
                        Map.ControlSpice(Head);
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
                        Map.ControlSpice(Head);
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
                        Map.ControlSpice(Head);
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
                        Map.ControlSpice(Head);
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
    }
}
