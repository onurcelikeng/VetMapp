using System;
using System.Collections.Generic;
using VetMapp.Core;
using VetMapp.Models;
using Windows.UI.Xaml.Controls;

namespace VetMapp.Views
{
    public sealed partial class SearchInformationView : Page
    {
        private static List<InformationModel> informations = new List<InformationModel>();
        public static InformationModel information;


        public SearchInformationView()
        {
            this.InitializeComponent();
            this.Loaded += SearchInformationView_Loaded;
        }


        private void SearchInformationView_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(informations.Count == 0)
            {
                GetInformationRequest();
            }

            else
            {
                foreach (var info in informations)
                {
                    if (info.ForLovers == true)
                    {
                        listView.Items.Add(info);
                    }
                }
            }
        }

        private async void GetInformationRequest()
        {
            progress.IsIndeterminate = true;

            try
            {
                informations = await DataClient.Instance.Informations();

                foreach(var info in informations)
                {
                    if(info.ForLovers == true)
                    {
                        listView.Items.Add(info);
                    }
                }
            }

            catch (Exception)
            {

            }

            progress.IsIndeterminate = false;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            information = listView.SelectedItem as InformationModel;
            Frame.Navigate(typeof(InformationDetailsView));
        }
    }
}