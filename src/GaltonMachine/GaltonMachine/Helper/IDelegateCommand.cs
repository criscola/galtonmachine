using System.Windows.Input;

namespace GaltonMachine.Helper
{
    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
