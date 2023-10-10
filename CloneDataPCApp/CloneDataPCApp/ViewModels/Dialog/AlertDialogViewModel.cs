using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CloneDataPCApp.ViewModels
{
    public class AlertDialogViewModel : BaseViewModel
    {
        public AlertDialogViewModel()
        {
            // Commands
            ExecuteCommand = ReactiveCommand.CreateFromTask(ExecuteTask, CanExecute);
        }

        #region Properties
        [Reactive]
        public string DialogContent { get; set; }
        #endregion

        #region Commands

        [Reactive]
        public ICommand ExecuteCommand { get; private set; }

        #endregion

        #region Methods

        public Task ExecuteTask()
        {
            Close();
            return Task.CompletedTask;
        }

        #endregion
    }
}