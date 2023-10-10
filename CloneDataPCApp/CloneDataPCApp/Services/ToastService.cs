using CloneDataPCApp.Interfaces;
using CloneDataPCApp.Views;
using Splat;
using System;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Lifetime.Clear;
using ToastNotifications.Position;

namespace CloneDataPCApp.Services
{
    public class ToastService : IToastService, IEnableLogger
    {
        private const int DEFAULT_TIMEOUT = 60;
        private readonly Notifier notifier;
        private double width;

        public ToastService()
        {
            notifier = new Notifier(configuration =>
            {
                configuration.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(DEFAULT_TIMEOUT), MaximumNotificationCount.FromCount(1));
                configuration.PositionProvider = new PrimaryScreenPositionProvider(Corner.BottomRight, 10, 10);
                configuration.Dispatcher = Application.Current.Dispatcher;
                configuration.DisplayOptions.Width = width;
            });
        }

        public Task ShowAsync(string title, string message, MessageOptions options = null)
        {
#if DEBUG
            this.Log().Info("Show toast");
#endif
            width = 300;
            notifier.Notify(() => new ToastNotification(title, message, options));
            return Task.CompletedTask;
        }

        public Task ShowProgressAsync(string title, string message, int elapsedTime = 16, MessageOptions options = null)
        {
#if DEBUG
            this.Log().Info("Show toast");
#endif
            width = 400;
            notifier.Notify(() => new ToastNotification(title, message, elapsedTime, options));
            return Task.CompletedTask;
        }

        public void ClearAll()
        {
#if DEBUG
            this.Log().Info("Clear all toast");
#endif
            Application.Current.Dispatcher.Invoke(new Action(() => { notifier.ClearMessages(new ClearAll()); }));
        }

        public void ClearByMessage(string message)
        {
#if DEBUG
            this.Log().Info($"Clear toast: {message}");
#endif
            Application.Current.Dispatcher.Invoke(new Action(() => { notifier.ClearMessages(new ClearByMessage(message)); }));
        }
    }
}