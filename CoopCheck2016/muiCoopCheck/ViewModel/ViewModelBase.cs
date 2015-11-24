using System;
using System.ComponentModel;

namespace CoopCheck.WPF.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged 
    {
        [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
        public sealed class CallerMemberNameAttribute : Attribute { }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            var e = PropertyChanged;

            if (e != null)
            {
                e(this, new PropertyChangedEventArgs(name));
                Console.WriteLine(name);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
  
  
}