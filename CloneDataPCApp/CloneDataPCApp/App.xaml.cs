using CloneDataPCApp.Interfaces;
using CloneDataPCApp.Services;
using CloneDataPCApp.ViewModels;
using CloneDataPCApp.Views;
using Splat;
using Splat.Log4Net;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CloneDataPCApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IEnableLogger
    {
        public App()
        {
            InitializeComponent();

            // Observe task exception
            TaskScheduler.UnobservedTaskException += (o, e) =>
            {
                this.Log().Error(e.Exception);
            };

            // DI register
            DoDIRegister();

            // Update app language
            CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;

#if DEBUG
            // Delete registry
            //RegistryHelper.Instance.Delete();

            // Create dummy registry
            //RegistryHelper.Instance.Create(DeviceType.PC);
#endif

            // Start app
            Current.MainWindow = Locator.Current.GetService<HomeView>();
            Locator.Current.GetService<HomeView>().ViewModel.Show();
        }

        private void DoDIRegister()
        {
            // Register logger service
            log4net.Config.XmlConfigurator.Configure();
            Locator.CurrentMutable.UseLog4NetWithWrappingFullLogger();

            // Register singleton services
            Locator.CurrentMutable.RegisterLazySingleton(() => new ToastService(), typeof(IToastService));

            // Register singleton views
            Locator.CurrentMutable.RegisterLazySingleton(() => new HomeView(), typeof(HomeView));

            // Register views
            Locator.CurrentMutable.Register(() => new AlertDialogView(), typeof(AlertDialogView));

        }

        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button)
                {
                    var parent = VisualTreeHelper.GetParent(sender as Button);
                    while (!(parent is Window))
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                    }
                    SystemCommands.MinimizeWindow(parent as Window);
                }
            }
            catch (Exception ex)
            {
                this.Log().Error(ex);
            }
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button)
                {
                    var parent = VisualTreeHelper.GetParent(sender as Button);
                    while (!(parent is Window))
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                    }
                    SystemCommands.CloseWindow(parent as Window);
                }
            }
            catch (Exception ex)
            {
                this.Log().Error(ex);
            }
        }
    }
}