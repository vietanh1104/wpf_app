using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CloneDataPCApp.Views
{
    /// <summary>
    /// Interaction logic for TabControlUserControl.xaml
    /// </summary>
    public partial class TabControlUserControl : UserControl
    {
        public TabControlUserControl()
        {
            InitializeComponent();
            DefaultStyleKey = typeof(TabControlUserControl);
           
            Loaded += (o, e) =>
            {
                CalculateTabStripSize();
                RenderTabHeaderContent();
            };
            LayoutUpdated += (o, e) =>
            {
                selectedTab = (Border)FindName("selectedTab");
            };
        }
        public object TabStrips
        {
            get { return GetValue(TabStripsProperty); }
            set { SetValue(TabStripsProperty, value); }
        }

        public static readonly DependencyProperty TabStripsProperty =
            DependencyProperty.Register("TabStrips", typeof(object), typeof(TabControlUserControl), null);

        #region Property
        private int currentIndex = 0;
        private int lastIndex = -1;
        private double tabStripWidth;
        public double SelectedTabWidth
        {
            get { return (double)GetValue(SelectedTabWidthProperty); }
            private set { SetValue(SelectedTabWidthProperty, value); }
        }

        public static readonly DependencyProperty SelectedTabWidthProperty =
            DependencyProperty.Register("SelectedTabWidth", typeof(double), typeof(TabControlUserControl), new PropertyMetadata(100.0, null));

        private List<StackPanel> _tabs = new List<StackPanel>();
        public Border selectedTab;
        #endregion

        #region initialized
        private void CalculateTabStripSize()
        {
            tabStripWidth = (ActualWidth * 3 / 5) / ((StackPanel)this.TabStrips).Children.Count;
            SelectedTabWidth = (ActualWidth * 3 / 5 - 10) / ((StackPanel)this.TabStrips).Children.Count;
        }
        
        private void RenderTabHeaderContent()
        {
            // Set the width for each child
            foreach (UIElement child in ((StackPanel)this.TabStrips).Children)
            {
                if (child is FrameworkElement frameworkElement)
                {
                    ((StackPanel)child).Width = tabStripWidth;
                    _tabs.Add((StackPanel)child);
                }
            }
        }
        #endregion
    }
}
