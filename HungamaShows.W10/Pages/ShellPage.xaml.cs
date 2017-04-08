using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;
using AppStudio.Uwp.Navigation;

using HungamaShows.Navigation;

namespace HungamaShows.Pages
{
    public sealed partial class ShellPage : Page
    {
        public static ShellPage Current { get; private set; }

        public ShellControl ShellControl
        {
            get { return shell; }
        }

        public Frame AppFrame
        {
            get { return frame; }
        }

        public ShellPage()
        {
            InitializeComponent();

            this.DataContext = this;
            ShellPage.Current = this;

            this.SizeChanged += OnSizeChanged;
            if (SystemNavigationManager.GetForCurrentView() != null)
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
                {
                    if (SupportFullScreen && ShellControl.IsFullScreen)
                    {
                        e.Handled = true;
                        ShellControl.ExitFullScreen();
                    }
                    else if (NavigationService.CanGoBack())
                    {
                        NavigationService.GoBack();
                        e.Handled = true;
                    }
                });
				
                NavigationService.Navigated += ((sender, e) =>
                {
                    if (NavigationService.CanGoBack())
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    }
                    else
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    }
                });
            }
        }

		public bool SupportFullScreen { get; set; }

		#region NavigationItems
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return (ObservableCollection<NavigationItem>)GetValue(NavigationItemsProperty); }
            set { SetValue(NavigationItemsProperty, value); }
        }

        public static readonly DependencyProperty NavigationItemsProperty = DependencyProperty.Register("NavigationItems", typeof(ObservableCollection<NavigationItem>), typeof(ShellPage), new PropertyMetadata(new ObservableCollection<NavigationItem>()));
        #endregion

		protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if DEBUG
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 320, Height = 500 });
#endif
            NavigationService.Initialize(typeof(ShellPage), AppFrame);
			NavigationService.NavigateToPage<HomePage>(e);

            InitializeNavigationItems();

            Bootstrap.Init();
        }		        
		
		#region Navigation
        private void InitializeNavigationItems()
        {
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"Home",
                "Home",
                (ni) => NavigationService.NavigateToRoot(),
                AppNavigation.IconFromSymbol(Symbol.Home)));
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"8e7519c2-9676-401a-b777-225dd977e5b3",
                "Shinchan",                
                AppNavigation.ActionFromPage("ShinchanListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/main.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"926a35b5-309c-414b-9a6f-d7e77fde955d",
                "Doraemon",                
                AppNavigation.ActionFromPage("DoraemonListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/jfchgfj.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"4f28b636-5284-4101-ba23-ea32d56949c6",
                "Kochikame",                
                AppNavigation.ActionFromPage("KochikameListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/3e.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"03dc8854-cd3a-43a8-baba-cd5710b4faf1",
                "Ninja Boy Rantaro",                
                AppNavigation.ActionFromPage("NinjaBoyRantaroListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/main-1.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"dc31ca90-6856-46fc-993f-5ffeaf488732",
                "Kiteretsu",                
                AppNavigation.ActionFromPage("KiteretsuListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/Orangekorosukelarge1.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"964a2917-b369-4961-a5d0-5c7bcf16393b",
                "Hagemaru",                
                AppNavigation.ActionFromPage("HagemaruListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/20080602121239189704.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"abff728c-70a5-44b0-8e67-7c03fd7985bc",
                "chhota bheem",                
                AppNavigation.ActionFromPage("ChhotaBheemListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/Chhota-Bheem-PNG.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"c506d9f1-43f2-4dfb-bc43-bb7fad8e794b",
                "Mighty Raju",                
                AppNavigation.ActionFromPage("MightyRajuListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/sub-logo2.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"d1610e08-7be9-4648-8e1b-277f5c6927de",
                "Oswald",                
                AppNavigation.ActionFromPage("OswaldListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/oswald-50f09818cec1e.png")) }));

            NavigationItems.Add(NavigationItem.Separator);

            NavigationItems.Add(AppNavigation.NodeFromControl(
				"About",
                "NavigationPaneAbout".StringResource(),
                new AboutPage(),
                AppNavigation.IconFromImage(new Uri("ms-appx:///Assets/about.png"))));
        }        
        #endregion        

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDisplayMode(e.NewSize.Width);
        }

        private void UpdateDisplayMode(double? width = null)
        {
            if (width == null)
            {
                width = Window.Current.Bounds.Width;
            }
            this.ShellControl.DisplayMode = width > 640 ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Overlay;
            this.ShellControl.CommandBarVerticalAlignment = width > 640 ? VerticalAlignment.Top : VerticalAlignment.Bottom;
        }

        private async void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.F11)
            {
                if (SupportFullScreen)
                {
                    await ShellControl.TryEnterFullScreenAsync();
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Escape)
            {
                if (SupportFullScreen && ShellControl.IsFullScreen)
                {
                    ShellControl.ExitFullScreen();
                }
                else
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
