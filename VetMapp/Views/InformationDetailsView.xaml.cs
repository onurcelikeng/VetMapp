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
    public sealed partial class InformationDetailsView : Page
    {

        public InformationDetailsView()
        {
            this.InitializeComponent();
            this.Loaded += InformationDetailsView_Loaded;
        }


        private void InformationDetailsView_Loaded(object sender, RoutedEventArgs e)
        {
            header.Text = SearchInformationView.information.Name;
            listView.ItemsSource = SearchInformationView.information.Titles;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listView.SelectedIndex;
            InformationWebView.Url = SearchInformationView.information.HtmlUrls[index].ToString();
            Frame.Navigate(typeof(InformationWebView));
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}