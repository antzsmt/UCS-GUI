using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Xml;
using UCS.Sys;

namespace UCS.UI
{
    /// <summary>
    /// Logica di interazione per SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {

        public static SplashScreen SS = new SplashScreen();

        public SplashScreen()
        {
            this.InitializeComponent();
            SS = this;
            this.label_version.Content = "UCS " + ConfUCS.VersionUCS;
            this.Opacity = 0;
            this.OpInW();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Thread T = new Thread(() => {
                Preload PT = new Preload();
                PT.PreloadThings();
                });
            T.Start();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            this.OpOutW(sender, e);
        }

        private void OpInW()
        {
            var OpIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
            this.BeginAnimation(OpacityProperty, OpIn);
        }

        private void OpOutW(object sender, CancelEventArgs e)
        {
            this.Closing -= this.Window_Closing;
            e.Cancel = true;
            var OpOut = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            OpOut.Completed += (s, _) => { this.Close(); MainWindow.RemoteWindow.Show(); MainWindow.IsFocusOk = true; };
            this.BeginAnimation(OpacityProperty, OpOut);
        }
    }
}

