using System;

namespace HandyCommand
{


    public sealed class Command<T> : Command
    {
        public Command(Action<T> execute) : base((obj) => execute((T)obj))
        {
            if (execute == null) if (execute == null) throw new ArgumentNullException(nameof(execute));

        }

        public Command(Action<T> execute, Func<T, bool> canExecute) : base((obj) => execute((T)obj), (obj) => canExecute((T)obj))
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }


    }

    public class Command : BaseCommand, ICommand
    {
        Action<object> execute;


        public Command(Action<object> execute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));

            this.execute = execute;
        }
        public Command(Action execute) : this((obj) => execute())
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
        }


        public Command(Action<object> execute, Func<object, bool> canExecute) : this(execute)
        {
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));

            this.canExecute = canExecute;

        }
        public Command(Action execute, Func<bool> canExecute) : this((obj) => execute(), (obj) => canExecute())
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));

        }     

        public void Execute(object parameter)
        {
            try
            {
                isExecuting = true;
                OnCanExecuteChanged();
                execute(parameter);

            }
            finally
            {
                isExecuting = false;
                OnCanExecuteChanged();
            }
        }

    }
}
