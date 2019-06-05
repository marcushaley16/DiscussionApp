using DiscussionApp.Data;
using DiscussionApp.Models;
using DiscussionApp.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionApp.Services
{
    public class FilmService
    {
        public bool CreateFilm(FilmCreate model)
        {
            var entity =
                    new Film()
                    {
                        Title = model.Title,
                        Director = model.Director,
                        Writer = model.Writer,
                        Stars = model.Stars,
                        Cinematographer = model.Cinematographer,
                        Editor = model.Editor,
                        Synopsis = model.Synopsis,
                        Genre1 = model.Genre1,
                        Genre2 = model.Genre2,
                        Released = model.Released,
                        Year = model.Year,
                        Runtime = model.Runtime,
                        Rating = model.Rating
                    };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Films.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FilmListItem> GetFilms()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Films
                        .Where(d => d.FilmId != 1)
                        .Select(
                            e =>
                                new FilmListItem
                                {
                                    FilmId = e.FilmId,
                                    Title = e.Title,
                                    Director = e.Director,
                                    Genre1 = e.Genre1,
                                    Genre2 = e.Genre2,
                                    Year = e.Year
                                }
                        );

                return query.ToArray();
            }
        }

        public FilmDetail GetFilmById(int filmId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Films
                        .Single(e => e.FilmId == filmId);
                    return
                        new FilmDetail
                        {
                            FilmId = entity.FilmId,
                            Title = entity.Title,
                            Director = entity.Director,
                            Writer = entity.Writer,
                            Stars = entity.Stars,
                            Cinematographer = entity.Cinematographer,
                            Editor = entity.Editor,
                            Synopsis = entity.Synopsis,
                            Genre1 = entity.Genre1,
                            Genre2 = entity.Genre2,
                            //Released = entity.Released,
                            Year = entity.Year,
                            Runtime = entity.Runtime,
                            Rating = entity.Rating
                        };
            }
        }

        public bool UpdateFilm(FilmEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Films
                        .Single(e => e.FilmId == model.FilmId);

                entity.Title = model.Title;
                entity.Director = model.Director;
                entity.Writer = model.Writer;
                entity.Stars = model.Stars;
                entity.Cinematographer = model.Cinematographer;
                entity.Editor = model.Editor;
                entity.Synopsis = model.Synopsis;
                entity.Genre1 = model.Genre1;
                entity.Genre2 = model.Genre2;
                entity.Released = model.Released;
                entity.Year = model.Year;
                entity.Runtime = model.Runtime;
                entity.Rating = model.Rating;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteFilm(int filmId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Films
                        .Single(e => e.FilmId == filmId);

                    ctx.Films.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        // Helper Method
        public string GetFilmTitle(int filmId)
        {
            using (var ctx = new ApplicationDbContext())
                return ctx.Films.Where(e => e.FilmId == filmId).Single().Title;
        }
    }
}
