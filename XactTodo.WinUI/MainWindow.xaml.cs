﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XactTodo.Models;

namespace XactTodo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        const int GWL_HWNDPARENT = -8;
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpWindowClass, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public MainWindow()
        {
            InitializeComponent();
            this.Top = 10;
            this.Left = SystemParameters.PrimaryScreenWidth - 10 - this.Width;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Init();
            var handle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            IntPtr hprog = FindWindowEx(
                FindWindowEx(
                    FindWindow("Progman", "Program Manager"),
                    IntPtr.Zero, "SHELLDLL_DefView", ""
                ),
                IntPtr.Zero, "SysListView32", "FolderView"
            );
            SetWindowLong(handle, GWL_HWNDPARENT, hprog);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        const int SE_SHUTDOWN_PRIVILEGE = 0x13;
        const int WM_WINDOWPOSCHANGED = 0x47;
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        bool inProc = false;
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_WINDOWPOSCHANGED)
            {
                if (inProc)
                    return IntPtr.Zero;
                inProc = true;
                var handle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
                SetWindowPos(handle, 1, 0, 0, 0, 0, SE_SHUTDOWN_PRIVILEGE);
                inProc = false;
            }
            return IntPtr.Zero;
        }

        /*
         * 虽然使用了WindowChrome已经可以拖动及缩放窗体，但如果将CaptionHeight属性设置过大，
         * 将会使右上角的按钮无法点击，所以仅将CaptionHeight属性设为8，并保留此方法用于拖动窗体
         */
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Point position = e.GetPosition(this);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < this.ActualWidth && position.Y >= 0 && position.Y < this.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        ImageSource imgBackground;
        Rect rectImg;

        /*void Init()
        {
            bBrush = new SolidColorBrush(Color.FromRgb(147, 174, 97));

            imgBackground = new BitmapImage(new Uri("pack://application:,,,/Res/glass.png"));

            txtLocation = new Point(45, 60);
            rectBk = new Rect(32, 32, this.Width - 90, this.Height - 102);
            rectImg = new Rect(0, 0, this.Width, this.Height);
            this.Background = null;
            this.InvalidateVisual();
        }*/

        protected override void OnRender(DrawingContext dc)
        {
            if (imgBackground == null)
            {
                imgBackground = new BitmapImage(new Uri("pack://application:,,,/Res/Images/background.png"));
                this.Background = null;
            }
            rectImg = new Rect(0, 0, this.Width, this.Height);
            dc.DrawImage(imgBackground, rectImg);
        }

        private void ListBoxItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var matter = ((ListBoxItem)sender).Content as Matter;
            MessageBox.Show(matter?.Subject);
        }
    }
}
