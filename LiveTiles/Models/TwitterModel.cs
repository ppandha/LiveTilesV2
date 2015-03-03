using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace LiveTiles.Models
{
    public class Twitter
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Search Criteria")]
        public string SearchCriteria { get; set; }

    }

    public class TwitterDBContext : DbContext
    {
        public TwitterDBContext()
            : base() 
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TwitterDBContext>());         
        } 
        
        public DbSet<Twitter> Twitters { get; set; }
    }
}