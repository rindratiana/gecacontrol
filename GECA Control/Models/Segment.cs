using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GECA_Control.Models
{
    public class Segment : Caterpillar
    {
        public Segment(Coordinates head, Coordinates tail) : base(head, tail) { }
    }
}
