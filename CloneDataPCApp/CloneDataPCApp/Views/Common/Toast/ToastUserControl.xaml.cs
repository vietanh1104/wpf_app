using System.Windows.Input;
using ToastNotifications.Core;

namespace CloneDataPCApp.Views
{
    /// <summary>
    /// Interaction logic for ToastUserControl.xaml
    /// </summary>
    public partial class ToastUserControl : NotificationDisplayPart
    {
        public ToastUserControl(ToastNotification notification)
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