using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace LiveTiles.Models
{
    public class Noticeboard
    {
        public int ID { get; set; }

        [Required]
        [Display(Name ="Organization Unit")]
        public string OrgUnit { get; set; }

        [Display(Name = "Organization Name")]
        public string OrgName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class NoticeboardDBContext : DbContext
    {
        public NoticeboardDBContext()
            : base() 
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<NoticeboardDBContext>());         
        }

        public DbSet<Noticeboard> Noticeboards { get; set; }

        public DbSet<LiveTiles.Models.Newsfeed> Newsfeeds { get; set; }
    }
}