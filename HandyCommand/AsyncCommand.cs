using System;
using System.Threading.Tasks;

namespace HandyCommand
{
    public sealed class AsyncCommand<T> : AsyncCommand
    {
        public AsyncCommand(Func<T, Task> executeAsync) : base(async (obj) => await executeAsync((T)obj))
        {
            if (executeAsync == null) throw new ArgumentNullException(nameof(executeAsync));
        }
        public AsyncCommand(Func<T, Task> executeAsync, Func<T, bool> canExecute) : base(async obj => await executeAsync((T)obj), obj=>canExecute((T)obj))
        {
            if (executeAsync == null) throw new ArgumentNullException(nameof(executeAsync));
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));

        }
    }

    public class AsyncCommand : BaseCommand, IAsyncCommand
    {
        Func<object, Task> executeAsync;


        public AsyncCommand(Func<Task> executeAsync) : this(async (obj) => await executeAsync())
        {
            if (executeAsync == null) throw new ArgumentNullException(nameof(executeAsync));
        }

        public AsyncCommand(Func<object, Task> executeAsync)
        {
            if (executeAsync == null) throw new ArgumentNullException(nameof(executeAsync));
            this.executeAsync = executeAsync;
        }

        public AsyncCommand(Func<object, Task> executeAsync, Func<object, bool> canExecute) : this(executeAsync)
        {
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));
            this.canExecute = canExecute;
        }

        public AsyncCommand(Func<Task> executeAsync, Func<bool> canExecute) : this(async (obj)=> await executeAsync(), (obj)=>canExecute())
        {
            if (executeAsync == null) throw new ArgumentNullException(nameof(executeAsync));
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));
        }


        public async Task ExecuteAsync(object parameter)
        {
            try
            {
                isExecuting = true;
                OnCanExecuteChanged();
                await executeAsync(parameter);
            }
            finally
            {
                isExecuting = false;
                OnCanExecuteChanged();
            }
        }

    }
}
