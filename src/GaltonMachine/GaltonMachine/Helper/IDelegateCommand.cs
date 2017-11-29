using System.Windows.Input;

namespace GaltonMachineWPF.Helpers
{
    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
