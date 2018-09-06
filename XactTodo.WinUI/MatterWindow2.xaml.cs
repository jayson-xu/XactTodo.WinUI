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

namespace XactTodo
{
    /// <summary>
    /// MatterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MatterWindow : Window
    {
        private Matter matter;

        public MatterWindow()
        {
            InitializeComponent();
        }

        //private static MatterWindow Window { get { return _window ?? (_window = new MatterWindow()); } }

        public static Matter AddMatter()
        {
            var window = new MatterWindow();
            window.Owner = Application.Current.MainWindow;
            if (window.ShowDialog()??false)
            {
                return window.matter;
            }
            else
                return null;
        }
    }

}
