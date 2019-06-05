using DiscussionApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionApp.Models
{
    public class FilmDetail
    {
        [Display(Name="ID")]
        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Stars { get; set; }
        public string Cinematographer { get; set; }
        public string Editor { get; set; }
        public string Synopsis { get; set; }
        [Display(Name = "Genre" )]
        public FilmGenreType Genre1 { get; set; }
        public FilmGenreType Genre2 { get; set; }
        //public bool Released { get; set; }
        public string Year { get; set; }
        public int Runtime { get; set; }
        public string Rating { get; set; }
    }
}
