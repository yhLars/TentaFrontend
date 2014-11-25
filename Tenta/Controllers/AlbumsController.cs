using System;
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
    builder.EntitySet<Album>("Albums");
    builder.EntitySet<Artist>("Artists"); 
    builder.EntitySet<Track>("Tracks"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AlbumsController : ODataController
    {
        private ChinookEntities db = new ChinookEntities();

        // GET: odata/Albums
        [EnableQuery]
        public IQueryable<Album> GetAlbums()
        {
            return db.Albums;
        }

        // GET: odata/Albums(5)
        [EnableQuery]
        public SingleResult<Album> GetAlbum([FromODataUri] int key)
        {
            return SingleResult.Create(db.Albums.Where(album => album.AlbumId == key));
        }

        // PUT: odata/Albums(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Album> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Album album = db.Albums.Find(key);
            if (album == null)
            {
                return NotFound();
            }

            patch.Put(album);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(album);
        }

        // POST: odata/Albums
        public IHttpActionResult Post(Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Albums.Add(album);
            db.SaveChanges();

            return Created(album);
        }

        // PATCH: odata/Albums(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Album> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Album album = db.Albums.Find(key);
            if (album == null)
            {
                return NotFound();
            }

            patch.Patch(album);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(album);
        }

        // DELETE: odata/Albums(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Album album = db.Albums.Find(key);
            if (album == null)
            {
                return NotFound();
            }

            db.Albums.Remove(album);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Albums(5)/Artist
        [EnableQuery]
        public SingleResult<Artist> GetArtist([FromODataUri] int key)
        {
            return SingleResult.Create(db.Albums.Where(m => m.AlbumId == key).Select(m => m.Artist));
        }

        // GET: odata/Albums(5)/Tracks
        [EnableQuery]
        public IQueryable<Track> GetTracks([FromODataUri] int key)
        {
            return db.Albums.Where(m => m.AlbumId == key).SelectMany(m => m.Tracks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlbumExists(int key)
        {
            return db.Albums.Count(e => e.AlbumId == key) > 0;
        }
    }
}
