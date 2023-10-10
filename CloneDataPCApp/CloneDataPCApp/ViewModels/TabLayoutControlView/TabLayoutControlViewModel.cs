using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reactive.Linq;

namespace CloneDataPCApp.ViewModels
{
    public class TabLayoutControlViewModel : BaseViewModel
    {
        public TabLayoutControlViewModel()
        {
            // Commands
            ClickHereCommand = ReactiveCommand.CreateFromTask<string>(ClickHereTask, CanExecute);

            // Observable Properties
            this.WhenAnyValue(x => x.CurrentIndex)
                .Select(x => x == 0)
                .ToPropertyEx(this, x => x.IsShowNext);
            this.WhenAnyValue(x => x.CurrentIndex)
                .Select(x => x == 1)
                .ToPropertyEx(this, x => x.IsShowPrevious);
        }

        #region Properties
        [Reactive]
        public int CurrentIndex { get; set; }
        [ObservableAsProperty]
        public bool IsShowNext { get; }
        [ObservableAsProperty]
        public bool IsShowPrevious { get; }

        public delegate void ClickHereEventHandler(string content);
        public event ClickHereEventHandler OnClickHere;

        #endregion

        #region Lifecycle

        public override void OnVisibleChanged(bool isVisible)
        {
            base.OnVisibleChanged(isVisible);
            // TODO
        }

        #endregion

        #region Commands

        [Reactive]
        public ICommand ClickHereCommand { get; private set; }

        #endregion

        #region Methods

        public async Task ClickHereTask(string content)
        {
            OnClickHere?.Invoke(content);
        }
        #endregion  
    }
}
