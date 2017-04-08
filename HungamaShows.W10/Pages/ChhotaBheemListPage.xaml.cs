//---------------------------------------------------------------------------
//
// <copyright file="ChhotaBheemListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class ChhotaBheemListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public ChhotaBheemListPage()
        {
			ViewModel = ViewModelFactory.NewList(new ChhotaBheemSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("abff728c-70a5-44b0-8e67-7c03fd7985bc");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
