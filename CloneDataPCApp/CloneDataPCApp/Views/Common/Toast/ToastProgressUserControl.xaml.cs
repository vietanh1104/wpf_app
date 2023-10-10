using System.Windows.Input;
using ToastNotifications.Core;

namespace CloneDataPCApp.Views
{
    /// <summary>
    /// Interaction logic for ToastProgressUserControl.xaml
    /// </summary>
    public partial class ToastProgressUserControl : NotificationDisplayPart
    {
        public ToastProgressUserControl(ToastNotification notification, int elapsedTime)
        {
            InitializeComponent();
            Bind(notification);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
        }

        private void OnTouchDown(object sender, System.EventArgs e)
        {
            OnClose();
        }
    }
}