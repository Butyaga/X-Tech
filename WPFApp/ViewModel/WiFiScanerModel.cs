using Abstract;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFApp.ViewModel.ViewCommand;

namespace WPFApp.ViewModel;
class WiFiScanerModel() : INotifyPropertyChanged
{
    private IWiFiScaner? _scaner;
    private IDataBase? _db;
    private IEnumerable<INetData> _data = [];

    public void SetDependency(IWiFiScaner scaner, IDataBase db)
    {
        _scaner = scaner;
        _db = db;
    }

    #region Properties
    public IEnumerable<INetData> Data
    {
        get => _data;
        set
        {
            _data = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(BestNet));
        }
    }

    public string? BestNet
    {
        get => Data.MaxBy(x => x.Strength)?.Name;
    }
    #endregion

    #region Commands
    #region ScanCommand
    private RelayCommand? _scanCommand;

    public RelayCommand ScanCommand
    {
        get
        {
            _scanCommand ??= new RelayCommand(ScanCommand_Execute);
            return _scanCommand;
        }
    }

    public async void ScanCommand_Execute(object? parameter)
    {
        _scanCommand?.SetAvailability(false);
        try
        {
            if (_scaner != null)
            {
                var temp = await _scaner.ScanAsync();
                Data = temp;
            }
        }
        catch (Exception) { }
        finally
        {
            _scanCommand?.SetAvailability(true);
        }
    }
    #endregion

    #region SaveDBCommand
    private RelayCommand? _saveDBCommand;

    public RelayCommand SaveDBCommand
    {
        get
        {
            _saveDBCommand ??= new RelayCommand(SaveDBCommand_Execute);
            return _saveDBCommand;
        }
    }

    public async void SaveDBCommand_Execute(object? parameter)
    {
        _saveDBCommand?.SetAvailability(false);
        try
        {
            if (_db != null)
            {
                await _db.SaveAsync(Data);
            }
        }
        catch (Exception) { }
        finally
        {
            _saveDBCommand?.SetAvailability(true);
        }
    }
    #endregion
    #endregion

    #region INotifyPropertyChanged member and ...
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
