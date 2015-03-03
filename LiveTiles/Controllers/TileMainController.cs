using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tweetinvi;
using Tweetinvi.Core.Interfaces.Credentials;

namespace LiveTiles.Controllers
{
    public class TileType
    {
        public int Id { get; set; }
        public int RefreshPeriod { get; set; }
    }

    public class Notice
    {
        public string Heading { get; set; }
        public string Content { get; set; }
    }

    public class NoticeBoardTile : TileType
    {
        public List<Notice> Notices { get; set; }
    }

    public class CalendarEvent : TileType
    {
        public DateTime date { get; set; }
        public string Description { get; set; }
    }

    public class CalendarTile : TileType
    {
        public List<CalendarEvent> Events { get; set; }
    }

    public class TwitterTile : TileType
    {
        public List<string> UserIds { get; set; }
    }

    public class NewsFeedTile : TileType
    {
        public string Url { get; set; }
    }


    public class TileConfiguration
    {
        public string Description { get; set; }
        public int LayoutId { get; set; }
        public List<TileType> Tiles { get; set; }
    }

    public class TileMainController : Controller
    {
        private TileConfiguration tileConfiguration;
        private const string _secret = "LYphTmbmcChE8II99MH6ucGh39QIpcc59F0SCHet98L82apjFk";
        private const string _key = "kljv3yj5FtxLEmAOwQ78x4XkG";

        public TileMainController()
        {

            string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + @"..\Views";
            tileConfiguration = new TileConfiguration();
            tileConfiguration.Description = "2 x 2";
            tileConfiguration.LayoutId = 1;
            tileConfiguration.Tiles = new List<TileType>
            {
                new NoticeBoardTile{Id = 1, RefreshPeriod = 0, Notices = new List<Notice>{new Notice{Heading = "heading 1", Content = "content 1"}, new Notice{Heading = "heading 2", Content = "content 2"}}},
                new CalendarTile{Id = 2, RefreshPeriod = 5000, Events = new List<CalendarEvent>{new CalendarEvent{date = new DateTime(2015,11,1), Description = "some event"}}},
                new TwitterTile{Id = 3, RefreshPeriod = 0, UserIds = new List<string>{"piersmorgan"}},
                new NewsFeedTile{Id = 4, RefreshPeriod = 4000, Url = "http://feeds.bbci.co.uk/news/rss.xml"},
            };

            TwitterCredentials.SetCredentials("Access_Token", "Access_Token_Secret", "Consumer_Key", "Consumer_Secret");
        }

        // GET: TileMain
        public ActionResult Index()
        {
            return View(tileConfiguration);
        }

        //    string url = "http://fooblog.com/feed";
        //XmlReader reader = XmlReader.Create(url);
        //SyndicationFeed feed = SyndicationFeed.Load(reader);
        //reader.Close();
        //foreach (SyndicationItem item in feed.Items)
        //{
        //    String subject = item.Title.Text;    
        //    String summary = item.Summary.Text;
        //    ...                
        //}
        public ActionResult GetView(int tileId)
        {
            // which tile is it?
            var tile = tileConfiguration.Tiles.Single(a => a.Id == tileId);

            if (tile is NoticeBoardTile)
            {
                return PartialView("_NoticeBoardTilePartialView", tile as NoticeBoardTile);
            }
            if (tile is CalendarTile)
            {
                return PartialView("_CalendarTilePartialView", tile as CalendarTile);
            }
            if (tile is TwitterTile)
            {
                var twitterTile = tile as TwitterTile;
                var tweets = GetTweets(twitterTile);
                return PartialView("_TwitterTilePartialView", tweets);
            }
            if (tile is NewsFeedTile)
            {
                return PartialView("_NewsFeedTilePartialView", tile as NewsFeedTile);
            }

            return PartialView("_TilePartialView");
        }

        private List<TweetDisplay> GetTweets(TwitterTile tile)
        {
            var credentials = CreateApplicationCredentials(_key, _secret);

            // Setup your credentials
            TwitterCredentials.SetCredentials(credentials.AuthorizationKey, credentials.AuthorizationSecret,
                credentials.ConsumerKey, credentials.ConsumerSecret);

            // Search the tweets containing tweetinvi
            var items = Search.SearchTweets("piersmorgan");
            var results = new List<TweetDisplay>();

            foreach (var item in items)
            {
                var td = new TweetDisplay {Author = item.Creator.Name, Tweet = item.Text, ImageUrl = item.Creator.ProfileImageUrl};
                results.Add(td);
            }

            return results;
        }

        // This method shows you how to create Application credentials. 
        // This type of credentials do not take a AccessKey or AccessSecret.
        private ITemporaryCredentials CreateApplicationCredentials(string consumerKey, string consumerSecret)
        {
            return CredentialsCreator.GenerateApplicationCredentials(consumerKey, consumerSecret);
        }
    }

    public class TweetDisplay
    {
        public string Tweet { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
    }
}