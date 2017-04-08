//---------------------------------------------------------------------------
//
// <copyright file="HagemaruListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>3/6/2017 2:59:21 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.YouTube;
using HungamaShows.Sections;
using HungamaShows.ViewModels;
using AppStudio.Uwp;

namespace HungamaShows.Pages
{
    public sealed partial class HagemaruListPage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings =
      Windows.Storage.ApplicationData.Current.LocalSettings;
        public ListViewModel ViewModel { get; set; }
        public HagemaruListPage()
        {
			ViewModel = ViewModelFactory.NewList(new HagemaruSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
            if (localSettings.Values["Language"].Equals("Hindi"))
            {
                Language_button.Label = "Tamil";

            }
            else if (localSettings.Values["Language"].Equals("Tamil"))
            {
                Language_button.Label = "Hindi";
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("964a2917-b369-4961-a5d0-5c7bcf16393b");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (localSettings.Values["Language"].Equals("Hindi"))
            {
                localSettings.Values["Language"] = "Tamil";
                Language_button.Label = "Hindi";

            }
            else
            {
                localSettings.Values["Language"] = "Hindi";
                Language_button.Label = "Tamil";
            }
        }

    }
}
