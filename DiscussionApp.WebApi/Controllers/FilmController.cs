﻿using DiscussionApp.Models;
using DiscussionApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DiscussionApp.WebApi.Controllers
{
    [Authorize]
    public class FilmController : ApiController
    {
        public IHttpActionResult GetAll()
        {
            FilmService filmService = CreateFilmService();
            var films = filmService.GetFilms();
            return Ok(films);
        }

        public IHttpActionResult Get(int id)
        {
            FilmService filmService = CreateFilmService();
            var film = filmService.GetFilmById(id);
            return Ok(film);
        }

        public IHttpActionResult Post(FilmCreate film)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateFilmService();

            if (!service.CreateFilm(film))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(FilmEdit film)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateFilmService();

            if (!service.UpdateFilm(film))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateFilmService();

            if (!service.DeleteFilm(id))
                return InternalServerError();

            return Ok();
        }

        private FilmService CreateFilmService()
        {
            var filmService = new FilmService();
            return filmService;
        }
    }
}
