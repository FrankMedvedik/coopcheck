using System;
using System.ComponentModel;

namespace CoopCheck.WPF.Pages
{
    public abstract class ViewModelBase : INotifyPropertyChanged 
    {
        [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
        public sealed class CallerMemberNameAttribute : Attribute { }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            var e = PropertyChanged;
            if (e != null) e(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
  
  
}