using System.Threading.Tasks;
using ToastNotifications.Core;

namespace CloneDataPCApp.Interfaces
{
    public interface IToastService
    {
        public Task ShowAsync(string title, string message, MessageOptions options = null);
        public Task ShowProgressAsync(string title, string message, int elapsedTime = 16, MessageOptions options = null);
        public void ClearAll();
        public void ClearByMessage(string message);
    }
}