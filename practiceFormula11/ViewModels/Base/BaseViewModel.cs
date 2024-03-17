using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ForAdelina.ViewModels.Base
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, string[] toNotify = null, [CallerMemberName] string propertyNane = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            if(toNotify != null)
            {
                foreach(var item in toNotify)
                {
                    OnPropertyChanged(item);
                }
            }
            OnPropertyChanged(propertyNane);
            return true;
        }
    }
}
