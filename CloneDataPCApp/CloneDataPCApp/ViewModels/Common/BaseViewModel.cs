using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CloneDataPCApp.ViewModels
{
    public class BaseViewModel : ReactiveObject, IEnableLogger
    {
        public BaseViewModel()
        {
            // Commands
            ShowHelpCommand = ReactiveCommand.CreateFromTask(ShowHelpTask, CanExecute);

            // Observable Properties
            this.WhenAnyValue(x => x.IsHelpSelected)
                .Select(x => IsHelpSelected ? Visibility.Hidden : Visibility.Visible)
                .ToPropertyEx(this, x => x.IsShowContent);

            this.WhenAnyValue(x => x.IsHelpSelected)
                .Select(x => !IsHelpSelected ? Visibility.Hidden : Visibility.Visible)
                .ToPropertyEx(this, x => x.IsShowHelp);

            this.WhenAnyValue(x => x.IsHelpSelected)
                .Select(x => !IsHelpSelected ? new SolidColorBrush(Colors.Transparent) : (SolidColorBrush)new BrushConverter().ConvertFrom("#0A000000"))
               .ToPropertyEx(this, x => x.HelpButtonColor);
        }

        #region Lifecycle

        public virtual void Initialize() { }

        public virtual void OnActivated() { }

        public virtual void OnDeactivated() { }

        public virtual void OnClosing() { }

        public virtual void OnClosed() { }

        public virtual void OnVisibleChanged(bool isVisible) { }

        #endregion

        #region Properties

        [Reactive]
        public string Title { get; set; }

        [Reactive]
        public string HeaderTitle { get; set; }

        [Reactive]
        public string HeaderDescription { get; set; }

        [Reactive]
        public Visibility AuxiliaryButtonVisibility { get; set; }

        [Reactive]
        public bool IsBusy { get; set; }

        public IObservable<bool> CanExecute => this.WhenAnyValue(x => x.IsBusy, p => !p);

        public event EventHandler OnRequestShow;

        public event EventHandler OnRequestClose;

        public event EventHandler OnRequestHide;

        [Reactive]
        public bool IsHelpSelected { get; set; } = false;
        [ObservableAsProperty]
        public Visibility IsShowContent { get; }
        [ObservableAsProperty]
        public Visibility IsShowHelp { get; }
        [ObservableAsProperty]
        public SolidColorBrush HelpButtonColor { get; }

        #endregion

        #region Commands

        [Reactive]
        public ICommand ShowHelpCommand { get; private set; }

        #endregion

        #region Methods

        public async Task ShowHelpTask()
        {
            IsHelpSelected = !IsHelpSelected;
        }

        public void Show()
        {
            OnRequestShow?.Invoke(this, EventArgs.Empty);
        }

        public void Close()
        {
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }
        public void Hide()
        {
            OnRequestHide?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}