using System.Windows;
using System.Windows.Input;

namespace ModernWPF_MVVM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        DragMove();
        //    }
        //}

        private bool mRestoreForDragMove;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton != MouseButtonState.Pressed)
            {
                if (e.ClickCount == 2)
                {
                    if (ResizeMode != ResizeMode.CanResize &&
                        ResizeMode != ResizeMode.CanResizeWithGrip)
                    {
                        return;
                    }

                    WindowState = WindowState == WindowState.Maximized
                        ? WindowState.Normal
                        : WindowState.Maximized;
                }
                else
                {
                    mRestoreForDragMove = WindowState == WindowState.Maximized;
                    DragMove();
                }
            }
        }


        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mRestoreForDragMove = false;
        }


        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (mRestoreForDragMove && e.LeftButton == MouseButtonState.Pressed)
            {
                mRestoreForDragMove = false;

                var point = PointToScreen(e.MouseDevice.GetPosition(this));
                var newLeft = point.X - (RestoreBounds.Width * 0.5);
                var newTop = point.Y;

                if (WindowState == WindowState.Maximized)
                {
                    Left = newLeft;
                    Top = newTop;
                    WindowState = WindowState.Normal;
                    DragMove();
                }
            }

            #region 
            //if (mRestoreForDragMove)
            //{
            //    mRestoreForDragMove = false;

            //    var point = PointToScreen(e.MouseDevice.GetPosition(this));

            //    Left = point.X - (RestoreBounds.Width * 0.5);
            //    Top = point.Y;

            //    WindowState = WindowState.Normal;

            //    DragMove();
            //}
            #endregion
        }
    }
}
