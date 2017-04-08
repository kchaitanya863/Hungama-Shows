using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HungamaShows.Sections;
namespace HungamaShows.ViewModels
{
    public class SearchViewModel : PageViewModelBase
    {
        public SearchViewModel() : base()
        {
			Title = "Hungama Shows";
            Shinchan = ViewModelFactory.NewList(new ShinchanSection());
            Doraemon = ViewModelFactory.NewList(new DoraemonSection());
            Kochikame = ViewModelFactory.NewList(new KochikameSection());
            NinjaBoyRantaro = ViewModelFactory.NewList(new NinjaBoyRantaroSection());
            Kiteretsu = ViewModelFactory.NewList(new KiteretsuSection());
            Hagemaru = ViewModelFactory.NewList(new HagemaruSection());
            ChhotaBheem = ViewModelFactory.NewList(new ChhotaBheemSection());
            MightyRaju = ViewModelFactory.NewList(new MightyRajuSection());
            Oswald = ViewModelFactory.NewList(new OswaldSection());
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel Shinchan { get; private set; }
        public ListViewModel Doraemon { get; private set; }
        public ListViewModel Kochikame { get; private set; }
        public ListViewModel NinjaBoyRantaro { get; private set; }
        public ListViewModel Kiteretsu { get; private set; }
        public ListViewModel Hagemaru { get; private set; }
        public ListViewModel ChhotaBheem { get; private set; }
        public ListViewModel MightyRaju { get; private set; }
        public ListViewModel Oswald { get; private set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
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
        }
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
