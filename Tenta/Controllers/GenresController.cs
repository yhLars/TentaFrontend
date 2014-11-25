﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Tenta;

namespace Tenta.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Tenta;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Genre>("Genres");
    builder.EntitySet<Track>("Tracks"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class GenresController : ODataController
    {
        private ChinookEntities db = new ChinookEntities();

        // GET: odata/Genres
        [EnableQuery]
        public IQueryable<Genre> GetGenres()
        {
            return db.Genres;
        }

        // GET: odata/Genres(5)
        [EnableQuery]
        public SingleResult<Genre> GetGenre([FromODataUri] int key)
        {
            return SingleResult.Create(db.Genres.Where(genre => genre.GenreId == key));
        }

        // PUT: odata/Genres(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Genre> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Genre genre = db.Genres.Find(key);
            if (genre == null)
            {
                return NotFound();
            }

            patch.Put(genre);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(genre);
        }

        // POST: odata/Genres
        public IHttpActionResult Post(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Genres.Add(genre);
            db.SaveChanges();

            return Created(genre);
        }

        // PATCH: odata/Genres(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Genre> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Genre genre = db.Genres.Find(key);
            if (genre == null)
            {
                return NotFound();
            }

            patch.Patch(genre);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(genre);
        }

        // DELETE: odata/Genres(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Genre genre = db.Genres.Find(key);
            if (genre == null)
            {
                return NotFound();
            }

            db.Genres.Remove(genre);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Genres(5)/Tracks
        [EnableQuery]
        public IQueryable<Track> GetTracks([FromODataUri] int key)
        {
            return db.Genres.Where(m => m.GenreId == key).SelectMany(m => m.Tracks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenreExists(int key)
        {
            return db.Genres.Count(e => e.GenreId == key) > 0;
        }
    }
}
