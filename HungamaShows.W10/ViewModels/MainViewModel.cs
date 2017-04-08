using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.YouTube;
using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.LocalStorage;
using HungamaShows.Sections;


namespace HungamaShows.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public ListViewModel Shinchan { get; private set; }
        public ListViewModel Doraemon { get; private set; }
        public ListViewModel Kochikame { get; private set; }
        public ListViewModel NinjaBoyRantaro { get; private set; }
        public ListViewModel Kiteretsu { get; private set; }
        public ListViewModel Hagemaru { get; private set; }
        public ListViewModel ChhotaBheem { get; private set; }
        public ListViewModel MightyRaju { get; private set; }
        public ListViewModel Oswald { get; private set; }
        public ListViewModel Feedback { get; private set; }

        public MainViewModel(int visibleItems) : base()
        {
            Title = "Hungama Shows";
            Shinchan = ViewModelFactory.NewList(new ShinchanSection(), visibleItems);
            Doraemon = ViewModelFactory.NewList(new DoraemonSection(), visibleItems);
            Kochikame = ViewModelFactory.NewList(new KochikameSection(), visibleItems);
            NinjaBoyRantaro = ViewModelFactory.NewList(new NinjaBoyRantaroSection(), visibleItems);
            Kiteretsu = ViewModelFactory.NewList(new KiteretsuSection(), visibleItems);
            Hagemaru = ViewModelFactory.NewList(new HagemaruSection(), visibleItems);
            ChhotaBheem = ViewModelFactory.NewList(new ChhotaBheemSection(), visibleItems);
            MightyRaju = ViewModelFactory.NewList(new MightyRajuSection(), visibleItems);
            Oswald = ViewModelFactory.NewList(new OswaldSection(), visibleItems);
            Feedback = ViewModelFactory.NewList(new FeedbackSection());

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = RefreshCommand,
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

		#region Commands
		public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var refreshDataTasks = GetViewModels()
                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

                    await Task.WhenAll(refreshDataTasks);
					LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
                    OnPropertyChanged("LastUpdated");
                });
            }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);
			LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Shinchan;
            yield return Doraemon;
            yield return Kochikame;
            yield return NinjaBoyRantaro;
            yield return Kiteretsu;
            yield return Hagemaru;
            yield return ChhotaBheem;
            yield return MightyRaju;
            yield return Oswald;
            yield return Feedback;
        }
    }
}
