using ForAdelina.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ForAdelina.Infrastructure.Commands;

namespace ForAdelina.ViewModels
{
    internal class MainWindowViewModel:BaseViewModel
    {

        public MainWindowViewModel()
        {
            StartCommand = new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);
        }
        #region Observs
        private int selectedVariant;
        public int SelectedVariant { get=>selectedVariant; set=>Set(ref selectedVariant, value, new string[] { nameof(IsASelected), nameof(IsBSelected), nameof(AText) }); }

        public bool IsASelected { get=>SelectedVariant==0?true:false; }
        public bool IsBSelected { get=>SelectedVariant==1?true:false; }
        public string AText { get => IsASelected ? "a" : "dA"; }

        private int dA;
        public int DA { get => dA; set => Set(ref dA, value);}

        private int a0;
        public int A0 { get => a0; set => Set(ref a0, value); }

        private int a1;
        public int A1 { get => a1; set => Set(ref a1, value); }
        private int dX;
        public int DX { get => dX; set => Set(ref dX, value);}
        private int x0;
        public int X0 { get => x0; set => Set(ref x0, value); }
        private int x1;
        public int X1 { get => x1; set => Set(ref x1, value); }
        #endregion
        #region privates

        #endregion
        #region Commands
        #region StartCommand
        public ICommand StartCommand { get; }
        private bool CanStartCommandExecute(object p) => true;
        private void OnStartCommandExecuted(object p)
        {
            //
        }
        #endregion

        #endregion
    }
}
