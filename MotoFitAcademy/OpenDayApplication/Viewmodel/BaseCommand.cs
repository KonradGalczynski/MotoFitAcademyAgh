using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenDayApplication.Viewmodel
{
    public class BaseCommand : ICommand
    {
        private readonly Action<object> _action;
        public BaseCommand(Action<object> action)
        {
            _action = action;
        }
        public BaseCommand(Action action)
        {
            _action = new Action<object>(p => action());
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
