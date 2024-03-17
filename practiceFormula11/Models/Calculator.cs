using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForAdelina.Models
{
    internal class Calculator
    {
        public Calculator(double X, double x0, double x1, double A)
        {
            this.dA = A;
            this.dX = X;
            this.x0 = x0;
            this.x1 = x1;
            taskType = TaskType.A;
        }
        public Calculator(double dX, double x0, double x1, double dA, double a0, double a1)
        {
            this.dA = dA;
            this.a0 = a0;
            this.a1 = a1;
            this.dX = dX;
            this.x0 = x0;
            this.x1 = x1;
            taskType = TaskType.B;
        }
        public double dA { get; private set; }
        public double a0 { get; private set; }
        public double a1 { get; private set; }
        public double dX { get; private set; }
        public double x0 { get; private set; }
        public double x1 { get; private set; }
        public enum TaskType
        {
            A, B
        }
        public TaskType taskType { get; private set; }
        public bool Start()
        {
            Points = new List<Point>();
            return taskType == TaskType.A ? StartA() : StartB();
        }

        private double formulaA(double x, double a) => Math.Abs(Math.Sin(2 * x)) * a / (1 + 2 * x + Math.Pow(x, 4));
        private double formulaB(double x, double a) => Math.Pow(4, x * a) * Math.Exp(x);
        private double formulaC(double x) => Math.Pow(x, 3);
        private double formulaChoise(double x, double a)
        {
            if (x < -1) return formulaC(x);
            if (x >= -1 && x <= 1) return formulaA(x, a);
            if (x > 1) return formulaB(x, a);
            return double.NaN;
        }
        private bool StartA()
        {
            for (double x = x0; x <= x1; x += dX)
            {
                double y = formulaChoise(x, dA);
                if (y == double.NaN) return false;
                Points.Add(new Point(x, y, dA));
            }
            return true;
        }
        private bool StartB()
        {
            for (double a = a0; a <= a1; a += dA)
            {
                for (double x = x0; x <= x1; x += dX)
                {
                    double y = formulaChoise(x, a);
                    if (y == double.NaN) return false;
                    Points.Add(new Point(x, y, a));
                }
            }
            return true;
        }

        public List<Point> Points { get; private set; }
    }
}
