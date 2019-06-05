using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionApp.Data
{
    public class Discussion
    {
        [Key]
        public Guid DiscussionId { get; set; }
        [Required]
        public Guid CreatorId { get; set; }
        public int FilmId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Titles must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "Titles are limited to 50 characters.")]
        public string DiscussionTitle { get; set; }
        [Required]
        public DateTimeOffset CreatedUTC { get; set; }
        public virtual Film Film { get; set; }
    }

    public class Post
    {
        [Key]
        public Guid PostId { get; set; }
        [Required]
        public Guid DiscussionId { get; set; }
        [Required]
        public Guid CreatorId { get; set; }
        [Required]
        public DateTimeOffset CreatedUTC { get; set; }
        public DateTimeOffset? ModifiedUTC { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Posts must be at least 5 characters long.")]
        public string Body { get; set; }
    }
}
