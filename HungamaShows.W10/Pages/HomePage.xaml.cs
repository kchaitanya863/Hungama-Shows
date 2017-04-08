//---------------------------------------------------------------------------
//
// <copyright file="HomePage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>3/6/2017 2:59:21 PM</createdOn>
//
//---------------------------------------------------------------------------

using System.Windows.Input;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;

using HungamaShows.ViewModels;

namespace HungamaShows.Pages
{
    public sealed partial class HomePage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings =
           Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder =
            Windows.Storage.ApplicationData.Current.LocalFolder;
        public HomePage()
        {
            ViewModel = new MainViewModel(12);            
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
			commandBar.DataContext = ViewModel;
			searchBox.SearchCommand = SearchCommand;
			this.SizeChanged += OnSizeChanged;
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);

           

            // Simple setting
            //localSettings.Values["exampleSetting"] = "Hello Windows";
            // Simple setting
            //Object value = localSettings.Values["exampleSetting"];
            if (localSettings.Values["Language"] == null) {
                localSettings.Values["Language"] = "Hindi";
            }
        }		
        public MainViewModel ViewModel { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
			//Page cache requires set commandBar in code
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
            ShellPage.Current.ShellControl.SelectItem("Home");
        }

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            searchBox.SearchWidth = e.NewSize.Width > 640 ? 230 : 190;
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(text =>
                {
                    searchBox.Reset();
                    ShellPage.Current.ShellControl.CloseLeftPane();                    
                    NavigationService.NavigateToPage("SearchPage", text, true);
                },
                SearchViewModel.CanSearch);
            }
        }

        private void Hindi_Language_Click(object sender, RoutedEventArgs e)
        {
           

        }
    }
}
