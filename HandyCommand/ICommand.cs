using System;
namespace HandyCommand
{
    public interface ICommand
    {
        //
        // Methods
        //
        bool CanExecute(object parameter);

        void Execute(object parameter);

        //
        // Events
        //
        event EventHandler CanExecuteChanged;
    }
}
