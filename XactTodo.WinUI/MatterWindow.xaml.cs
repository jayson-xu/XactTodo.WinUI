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
using System.Windows.Shapes;
using XactTodo.Models;
using XactTodo.Utils;

namespace XactTodo
{
    /// <summary>
    /// MatterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MatterWindow : Window
    {

        public MatterWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOkay_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MatterViewModel).Save();
            this.DialogResult = true;
        }

        public static Matter AddMatter()
        {
            var window = new MatterWindow
            {
                Owner = Application.Current.MainWindow
            };
            if (window.ShowDialog()??false)
            {
                return (window.DataContext as MatterViewModel)?.Matter;
            }
            else
                return null;
        }

        internal static void ShowMatter(Matter matter)
        {
            var window = new MatterWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = new MatterViewModel(matter),
            };
            window.ShowDialog();
        }

    }

}
