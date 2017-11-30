using System;
using System.Threading.Tasks;

namespace HandyCommand
{
    public interface IAsyncCommand
    {
        Task ExecuteAsync(object parameter);
        bool CanExecute(object parameter);
        event EventHandler CanExecuteChanged;
        event EventHandler ShouldExecute;

    }
}
