using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForAdelina.Models
{
    internal class Point
    {
        public Point(double x=0, double y=0, double a=0)
        {
            X = x;
            Y = y;
            A = a;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public double A { get; set; }
        public bool Equals(Point? p1)
        {
            return X==p1?.X && Y==p1?.Y && A==p1?.A;
        }

        public override string? ToString()
        {
            return "("+X+";"+Y+"){"+A+"}";
        }
    }
}
