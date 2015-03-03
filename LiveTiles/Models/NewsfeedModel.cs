using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace LiveTiles.Models
{
    public class Newsfeed
    {
        public int ID { get; set; }
   
        [Required]
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "RSS Source URL")]
        public string RssUrl { get; set; }
    }

    public class NewsfeedDBContext : DbContext
    {
        public NewsfeedDBContext()
            : base() 
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<NewsfeedDBContext>());         
        }
    
        public DbSet<Newsfeed> Newsfeeds { get; set; }
    }
}



