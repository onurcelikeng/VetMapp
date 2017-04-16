using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace VetMapp.Views
{
    public sealed partial class InformationWebView : Page
    {
        public static string Url;


        public InformationWebView()
        {
            this.InitializeComponent();
            webView.Navigate(new Uri(Url));
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}