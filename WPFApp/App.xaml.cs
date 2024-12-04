using System.Windows;
using WPFApp.ViewModel;
using FakeWiFiScaner;
using WiFiScaner;
using SQLite;

namespace WPFApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    const string _dbName = "netDB";
    const string _pdfFileName = "out.pdf";
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        WiFiScanerModel? wifiScanerModel = Resources["wifiScanerModel"] as WiFiScanerModel;
        //wifiScanerModel?.SetDependency(new FakeScaner(), new SQLiteDB(dbName));
        wifiScanerModel?.SetDependency(new Scaner(), new SQLiteDB(_dbName));

        PDFWriterModel? pdfWriterModel = Resources["pdfWriterModel"] as PDFWriterModel;
        pdfWriterModel?.SetDependency(new PDFPrinter.PDFWriter());
    }
}
