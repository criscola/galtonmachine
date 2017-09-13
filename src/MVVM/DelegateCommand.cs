using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    public class DelegateCommand : IDelegateCommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;
        private Action onBancaCarlo10;

        // Evento richiesto da ICommand
        public event EventHandler CanExecuteChanged;
        // Construttore
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public DelegateCommand(Action onBancaCarlo10)
        {
            this.onBancaCarlo10 = onBancaCarlo10;
        }

        // Metodi richiesti da ICommand
        #region ICommand
        public void Execute(object parameter)
        {
            execute(parameter);
        }
        public bool CanExecute(object parameter)
        {
            bool b = canExecute == null ? true : canExecute(parameter);
            return b;
        }
        #endregion
        // Metodo richiesto da IDelegateCommand
        #region IDelegateCommand
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
        #endregion
    }
}
