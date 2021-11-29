using System;
using System.Windows.Input;

namespace TedisNetDataSimulator
{
    public class RelayCommand : ICommand
    {
        #region Attributes
        private Action<object> _execute;
        private Predicate<object> _canExecute;
        #endregion

        #region Events & Delegates
        private event EventHandler CanExecuteChangedInternal;
        #endregion

        #region Constructors
        public RelayCommand(Action<object> execute) : this(execute, DefaultCanExecute) { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region Events
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            return _canExecute != null && _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChangedInternal;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public void Destroy()
        {
            _canExecute = _ => false;
            _execute = _ => { return; };
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
        #endregion
    }
}