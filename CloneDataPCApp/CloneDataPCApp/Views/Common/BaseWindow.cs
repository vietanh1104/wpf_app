using CloneDataPCApp.ViewModels;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shell;

namespace CloneDataPCApp.Views
{
    public class BaseWindow<TViewModel> : ReactiveWindow<TViewModel> where TViewModel : BaseViewModel
    {
        public BaseWindow(): base()
        {
            // UI
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.CanMinimize;
            WindowChrome.SetWindowChrome(this, new WindowChrome()
            {
                CaptionHeight = 32,
                ResizeBorderThickness = SystemParameters.WindowResizeBorderThickness
            });
            Template = (ControlTemplate)Application.Current.FindResource("BaseWindowTemplate");
            Background = (Brush)Application.Current.FindResource("AppBrush");

            // ViewModel initialization
            ViewModel = (TViewModel)Activator.CreateInstance(typeof(TViewModel));

            // UI parameters
            ViewModel.Title = ViewId.Titles[GetType()];
            ViewModel.HeaderTitle = ViewId.HeaderTitles[GetType()];
            ViewModel.HeaderDescription = ViewId.HeaderDescription[GetType()];
            ViewModel.AuxiliaryButtonVisibility = ViewId.Dialogs.Contains(GetType()) ? Visibility.Collapsed : Visibility.Visible;

            // Binding common methods
            ViewModel.OnRequestShow += (o, e) => Show();
            ViewModel.OnRequestClose += (o, e) => Close();
            ViewModel.OnRequestHide += (o, e) => Hide();
            IsVisibleChanged += (o, e) => ViewModel.OnVisibleChanged((bool)e.NewValue);
            SourceInitialized += (o, e) => ViewModel.Initialize();

            // Set DataContext
            DataContext = ViewModel;
        }

        #region Lifecycle

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            ViewModel?.OnActivated();
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            ViewModel?.OnDeactivated();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            ViewModel?.OnClosing();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ViewModel?.OnClosed();
        }

        #endregion

        public void OnHyperlinkRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }
}