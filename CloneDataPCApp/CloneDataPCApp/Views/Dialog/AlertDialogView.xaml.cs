using CloneDataPCApp.ViewModels;
using System.Windows;

namespace CloneDataPCApp.Views
{
    /// <summary>
    /// Interaction logic for AlertDialogView.xaml
    /// </summary>
    public partial class AlertDialogView : BaseWindow<AlertDialogViewModel>
    {
        public AlertDialogView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }
    }
}