//---------------------------------------------------------------------------
//
// <copyright file="NinjaBoyRantaroListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class NinjaBoyRantaroListPage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings =
    Windows.Storage.ApplicationData.Current.LocalSettings;
        public ListViewModel ViewModel { get; set; }
        public NinjaBoyRantaroListPage()
        {
			ViewModel = ViewModelFactory.NewList(new NinjaBoyRantaroSection());

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
			ShellPage.Current.ShellControl.SelectItem("03dc8854-cd3a-43a8-baba-cd5710b4faf1");
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
