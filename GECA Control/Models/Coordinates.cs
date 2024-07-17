using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GECA_Control.Models
{
    public class Coordinates
    {
        private int x;
        private int y;
        public bool IsHead {  get; set; }
        public char Value { get; set; }
        public int X
        {
            get { return x; }
            set
            {
                /*if (value < 0)
                    throw new ArgumentException("X must be a positive number.");*/
                x = value;
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                /*if (value < 0)
                    throw new ArgumentException("Y must be a positive number.");*/
                y = value;
            }
        }
        public Coordinates(int x, int y,char value)
        {
            X = x;
            Y = y;
            Value = value;
        }
        public Coordinates() { }
    }
}
