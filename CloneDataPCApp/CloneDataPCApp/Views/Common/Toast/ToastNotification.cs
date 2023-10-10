using ReactiveUI.Fody.Helpers;
using System.ComponentModel;
using ToastNotifications.Core;

namespace CloneDataPCApp.Views
{
    public class ToastNotification : NotificationBase, INotifyPropertyChanged
    {
        private NotificationDisplayPart displayPart;

        public override NotificationDisplayPart DisplayPart => displayPart;

        public ToastNotification(string title, string message, MessageOptions options) : base(message, options)
        {
            displayPart = new ToastUserControl(this);
            Title = title;
            ToastMessage = message;
        }

        public ToastNotification(string title, string message, int elapsedTime, MessageOptions options) : base(message, options)
        {
            displayPart = new ToastProgressUserControl(this, elapsedTime);
            Title = title;
            ToastMessage = message;
        }

        [Reactive]
        public string Title { get; set; }
        [Reactive]
        public string ToastMessage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}