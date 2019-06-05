using DiscussionApp.Models;
using DiscussionApp.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionApp.Services
{
    public class HomeService
    {
        public IEnumerable<DiscussionListItem> GetTrendingDiscussions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Discussions
                        .Select(
                            d =>
                                new DiscussionListItem
                                {
                                    DiscussionId = d.DiscussionId,
                                    FilmId = d.FilmId,
                                    FilmTitle = d.Film.Title,
                                    DiscussionTitle = d.DiscussionTitle,
                                    CreatorId = d.CreatorId,
                                    CreatorUsername = ctx.Users.Where(y => y.Id == d.CreatorId.ToString()).FirstOrDefault().UserName,
                                    CreatedUTC = d.CreatedUTC,
                                    PostCount = ctx.Posts.Where(p => p.DiscussionId == d.DiscussionId).Count(),
                                    LastPostCreatorUsername = ctx.Users.FirstOrDefault(user => user.Id == ctx.Posts.Where(post => post.DiscussionId == d.DiscussionId).OrderByDescending(post => post.CreatedUTC).FirstOrDefault().CreatorId.ToString()).UserName,
                                    //LastPostCreatorUsername = ctx.Users.Where(x => x.Id == ctx.Posts.Where(p => p.DiscussionId == d.DiscussionId).OrderByDescending(y => y.CreatedUTC).FirstOrDefault().CreatorId.ToString()).FirstOrDefault().UserName,
                                    LastPostUTC = ctx.Posts.Where(p => p.DiscussionId == d.DiscussionId).OrderByDescending(x => x.CreatedUTC).FirstOrDefault().CreatedUTC
                                }
                            );
                return query.OrderByDescending(x => x.PostCount).Take(6).ToArray();
            }
        }
    }
}
