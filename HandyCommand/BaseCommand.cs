﻿using System;
namespace HandyCommand
{
    public abstract class BaseCommand
    {
        protected Func<object, bool> canExecute;
        protected bool isExecuting;

        //Event declaration. No need for event delegate therefore using
        //base EventHandler
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (canExecute == null) return !isExecuting && true;
            return !isExecuting && canExecute(parameter);
        }


        //Method to raise event
        protected void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}