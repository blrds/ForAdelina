using ForAdelina.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ForAdelina.Infrastructure.Commands;
using OxyPlot;
using ForAdelina.Models;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Legends;

namespace ForAdelina.ViewModels
{
    internal class MainWindowViewModel:BaseViewModel
    {

        public MainWindowViewModel()
        {
            StartCommand = new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);
            DX = 0.5d;
            A0 = 2d;
            A1 = 3.5d;
            DA = 0.3d;
            X0 = -2;
            X1 = 2;
        }
        #region Observs
        private int selectedVariant;
        public int SelectedVariant { get=>selectedVariant; set=>Set(ref selectedVariant, value, new string[] { nameof(IsASelected), nameof(IsBSelected), nameof(AText) }); }

        public bool IsASelected { get=>SelectedVariant==0?true:false; }
        public bool IsBSelected { get=>SelectedVariant==1?true:false; }
        public string AText { get => IsASelected ? "a" : "dA"; }

        private double dA;
        public double DA { get => dA; set => Set(ref dA, value);}

        private double a0;
        public double A0 { get => a0; set => Set(ref a0, value); }

        private double a1;
        public double A1 { get => a1; set => Set(ref a1, value); }
        private double dX;
        public double DX { get => dX; set => Set(ref dX, value);}
        private double x0;
        public double X0 { get => x0; set => Set(ref x0, value); }
        private double x1;
        public double X1 { get => x1; set => Set(ref x1, value); }

        public PlotModel Chart { get; private set; }
        public List<Models.Point> Points { get; private set; } = new List<Models.Point>() { };
        #endregion
        #region privates
        private Calculator calc;
        #endregion
        #region Commands
        #region StartCommand
        public ICommand StartCommand { get; }
        private bool CanStartCommandExecute(object p)
        {
            if (x0 >= x1) return false;
            if (SelectedVariant == 1)
                if(a0>= a1) return false;
            return true;
        }
        private void OnStartCommandExecuted(object p)
        {
            var chart = new PlotModel();
            chart.Title = "";
            chart.Axes.Clear();
            chart.Axes.Add(new LinearAxis());
            chart.Axes.Add(new LinearAxis());
            chart.Axes[0].Position=AxisPosition.Bottom;
            chart.Axes[1].Position = AxisPosition.Left;
            chart.Legends.Add(new Legend());

            calc =SelectedVariant==0?new Calculator(DX, X0, X1, DA):new Calculator(DX, X0, X1, DA, A0, A1);
            calc.Start();
            Points = new List<Models.Point>();
            Points.AddRange(calc.Points);
            OnPropertyChanged(nameof(Points));

            if (SelectedVariant == 0)
            {
                LineSeries lineSeries = new LineSeries();
                foreach (var point in Points)
                {
                    lineSeries.Points.Add(new DataPoint(point.X, point.Y));
                }
                chart.Series.Add(lineSeries);
            }
            else
            {
                double hA = Points[0].A;
                chart.Series.Add(new LineSeries());
                (chart.Series.Last() as LineSeries).Title = hA.ToString();
                foreach (var point in Points)
                {
                    if (hA != point.A)
                    {
                        hA = point.A;
                        chart.Series.Add(new LineSeries());
                        (chart.Series.Last() as LineSeries).Title = hA.ToString();
                    }
                    (chart.Series.Last() as LineSeries).Points.Add(new DataPoint(point.X,point.Y));
                }
            }
            Chart = chart;
            OnPropertyChanged(nameof (Chart));
        }
        #endregion
        #endregion
    }
}
