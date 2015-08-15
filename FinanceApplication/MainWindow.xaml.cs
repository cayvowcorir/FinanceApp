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
using System.Windows.Threading;

namespace FinanceApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;       
           
        }

        void browserFrame_FragmentNavigation(object sender, FragmentNavigationEventArgs e)
        {
            var content = ((ContentControl)e.Navigator).Content;
            var fragmentElement = LogicalTreeHelper.FindLogicalNode((DependencyObject)content, e.Fragment) as FrameworkElement;
            if (fragmentElement == null)
            {
                // Redirect to error page
                // Note - You can't navigate from within a FragmentNavigation event handler,
                //        hence creation of an async dispatcher work item
                this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Send,
                    (DispatcherOperationCallback)delegate(object unused)
                    {
                        this.BrowserFrame.Navigate(new Uri("FragmentNotFoundPage.xaml", UriKind.Relative));
                        return null;
                    },
                    null);
                e.Handled = true;
            }
        }

        

       
    }
}
