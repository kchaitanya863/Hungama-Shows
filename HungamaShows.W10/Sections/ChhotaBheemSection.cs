using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.YouTube;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using HungamaShows.Navigation;
using HungamaShows.ViewModels;

namespace HungamaShows.Sections
{
    public class ChhotaBheemSection : Section<YouTubeSchema>
    {
		private YouTubeDataProvider _dataProvider;
		
		public ChhotaBheemSection()
		{
			_dataProvider = new YouTubeDataProvider(new YouTubeOAuthTokens
			{
				ApiKey = "AIzaSyBtkNT1n4nuRGmndLhS5PNlTTazT4Hbl0U"
			});
		}

		public override async Task<IEnumerable<YouTubeSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new YouTubeDataConfig
            {
                QueryType = YouTubeQueryType.Playlist,
                Query = @"PLYu4LNxWCBLeXJzy4oSB7CN_Klu8h0pt0",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<YouTubeSchema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override ListPageConfig<YouTubeSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<YouTubeSchema>
                {
                    Title = "chhota bheem",

                    Page = typeof(Pages.ChhotaBheemListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.ChhotaBheemDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<YouTubeSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, YouTubeSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = null;
                    viewModel.Description = "";
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(null);
                    viewModel.Content = item.EmbedHtmlFragment;
					viewModel.Source = item.ExternalUrl;
                });

                var actions = new List<ActionConfig<YouTubeSchema>>
                {
                    ActionConfig<YouTubeSchema>.Link("Go To Source", (item) => item.ExternalUrl.ToSafeString()),
                };

                return new DetailPageConfig<YouTubeSchema>
                {
                    Title = "chhota bheem",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
