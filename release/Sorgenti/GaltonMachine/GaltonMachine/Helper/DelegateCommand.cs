using System;

namespace GaltonMachine.Helper
{
    public class DelegateCommand : IDelegateCommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;
        private Action action;

        // Evento richiesto da ICommand
        public event EventHandler CanExecuteChanged;
        // Construttore
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public DelegateCommand(Action action)
        {
            this.action = action;
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
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
