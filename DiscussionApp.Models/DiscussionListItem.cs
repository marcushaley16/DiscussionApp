using DiscussionApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionApp.Models
{
    public class DiscussionListItem
    {
        [Display(Name = "ID")]
        public Guid DiscussionId { get; set; }
        public int FilmId { get; set; }
        public string FilmTitle { get; set; }
        [Display(Name = "Title")]
        public string DiscussionTitle { get; set; }
        public Guid CreatorId { get; set; } // hidden
        public string CreatorUsername { get; set; }
        [Display(Name = "Created")]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt} on {0:MMM dd, yyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedUTC { get; set; }
        [Display(Name = "Posts")]
        public int? PostCount { get; set; }
        public string LastPostCreatorUsername { get; set; }
        [Display(Name = "Last Post")]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt} on {0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? LastPostUTC { get; set; }
        public override string ToString() { return base.ToString(); }
    }
}
