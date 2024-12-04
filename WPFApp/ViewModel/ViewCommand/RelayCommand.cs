using System.Windows.Input;

namespace WPFApp.ViewModel.ViewCommand;

public delegate void DelegateExecuteCommand(object? parameter);
public delegate bool DelegateCanExecute(object? parameter);

public class RelayCommand : ICommand
{
    #region Private fields
    private readonly DelegateExecuteCommand _execute;
    private readonly DelegateCanExecute? _canExecuteDelegate;
    private bool _canExecuteBool;
    private EventHandler? _canExecuteChanged;
    #endregion

    #region Constractors
    public RelayCommand(DelegateExecuteCommand execute, bool initCanExecute = true)
    {
        _execute = execute;
        _canExecuteBool = initCanExecute;
    }

    public RelayCommand(DelegateExecuteCommand execute, DelegateCanExecute canExecute)
    {
        _execute = execute;
        _canExecuteDelegate = canExecute;
    }
    #endregion

    #region ICommand Members
    public event EventHandler? CanExecuteChanged
    {
        add
        {
            if (_canExecuteDelegate == null)
            {
                _canExecuteChanged += value;
                return;
            }
            CommandManager.RequerySuggested += value;
        }
        remove
        {
            if (_canExecuteDelegate == null)
            {
                _canExecuteChanged -= value;
                return;
            }
            CommandManager.RequerySuggested -= value;
        }
    }

    public bool CanExecute(object? parameter = null)
    {
        if (_canExecuteDelegate == null)
        {
            return _canExecuteBool;
        }
        return _canExecuteDelegate.Invoke(parameter);
    }

    public void Execute(object? parameter = null)
    {
        _execute.Invoke(parameter);
    }
    #endregion

    /// <summary>
    /// Изменение доступности команды.
    /// Не имеет смысла, если был в конструктор был передан предикат canExecute
    /// </summary>
    /// <param name="newStatus"></param>
    public void SetAvailability(bool newStatus)
    {
        if (_canExecuteBool ^ newStatus)
        {
            _canExecuteBool = newStatus;
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
