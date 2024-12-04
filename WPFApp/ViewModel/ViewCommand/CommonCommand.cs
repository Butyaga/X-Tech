using System.Windows;

namespace WPFApp.ViewModel.ViewCommand;
class CommonCommand
{
    #region Private Fields
    private RelayCommand? _closeWindow;
    #endregion

    #region CloseWindow
    public RelayCommand CloseWindow
    {
        get
        {
            _closeWindow ??= new RelayCommand(CloseWindow_Execute);
            return _closeWindow;
        }
    }

    public void CloseWindow_Execute(object? parameter)
    {
        if (parameter is Window wnd)
        {
            wnd.Close();
            return;
        }

        if (parameter is DependencyObject dependencyObject)
        {
            wnd = Window.GetWindow(dependencyObject);
            wnd.Close();
        }
    }
    #endregion
}
