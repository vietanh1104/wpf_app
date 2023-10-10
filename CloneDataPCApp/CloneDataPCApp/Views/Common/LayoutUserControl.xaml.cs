using System.Windows;
using System.Windows.Controls;

namespace CloneDataPCApp.Views
{
    /// <summary>
    /// Interaction logic for LayoutUserControl.xaml
    /// </summary>
    public partial class LayoutUserControl : UserControl
    {
        public LayoutUserControl()
        {
            InitializeComponent();
            DefaultStyleKey = typeof(LayoutUserControl);
        }
            
        public object LeftAction
        {
            get { return GetValue(LeftActionProperty); }
            set { SetValue(LeftActionProperty, value); }
        }

        public static readonly DependencyProperty LeftActionProperty =
            DependencyProperty.Register("LeftAction", typeof(object), typeof(LayoutUserControl), null);

        public object RightAction
        {
            get { return GetValue(RightActionProperty); }
            set { SetValue(RightActionProperty, value); }
        }

        public static readonly DependencyProperty RightActionProperty =
            DependencyProperty.Register("RightAction", typeof(object), typeof(LayoutUserControl), null);
    }
}
