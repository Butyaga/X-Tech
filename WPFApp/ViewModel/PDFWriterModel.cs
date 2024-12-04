using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFApp.Model;
using Abstract;
using WPFApp.ViewModel.ViewCommand;

namespace WPFApp.ViewModel;
class PDFWriterModel : INotifyPropertyChanged
{
    private IPDFWriter? _pdfWriter;
    private string _message = "Некий текст";
    private List<Human> _data = [new(1, "Иванов", "Иван")
                                , new(2, "Сидоров", "Сидор")
                                , new(3, "Петров", "Петр"),];

    public void SetDependency(IPDFWriter writer)
    {
        _pdfWriter = writer;
    }

    #region Properties
    public List<Human> Data
    {
        get => _data;
        set
        {
            _data = value;
            OnPropertyChanged();
        }
    }

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region SavePDFCommand
    private RelayCommand? _savePDFCommand;

    public RelayCommand SavePDFCommand
    {
        get
        {
            _savePDFCommand ??= new RelayCommand(SavePDFCommand_Execute);
            return _savePDFCommand;
        }
    }

    public void SavePDFCommand_Execute(object? parameter)
    {
        _savePDFCommand?.SetAvailability(false);
        try
        {
            _pdfWriter?.Write(_message, Data);
        }
        catch (Exception) { }
        finally
        {
            _savePDFCommand?.SetAvailability(true);
        }
    }
    #endregion

    #region INotifyPropertyChanged member and ...
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
