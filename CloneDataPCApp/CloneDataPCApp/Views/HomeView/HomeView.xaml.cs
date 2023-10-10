using Splat;
using System.Windows;
using System.ComponentModel;

namespace CloneDataPCApp.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class HomeView
    {
        public HomeView()
        {   
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            // Binding methods
            ViewModel.OnClickHere += (content) => OnClickHere(content);
            ViewModel.ProgressPercent = 10;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Cancel closing event
            e.Cancel = true;
            Hide();
            base.OnClosing(e);
        }

        private void OnClickHere(string content)
        {
            AlertDialogView dialog = Locator.Current.GetService<AlertDialogView>();
            dialog.ViewModel.DialogContent = content;
            dialog.ShowDialog();
        }
    }
}
