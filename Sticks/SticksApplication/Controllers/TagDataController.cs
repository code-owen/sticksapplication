using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SticksApplication.Models;

namespace SticksApplication.Controllers
{
    public class TagDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of all tags.
        /// </summary>
        /// <returns>
        /// A collection of all tags in the database.
        /// </returns>
        /// <example>
        /// GET: api/TagData/ListTags
        /// </example>
        [HttpGet]
        public IEnumerable<Tag> ListTags()
        {
            return db.Tags;
        }

        /// <summary>
        /// Finds a specific tag by its ID.
        /// </summary>
        /// <param name="id">The ID of the tag to find.</param>
        /// <returns>
        /// HEADER: 200 (OK) if found; 404 (Not Found) if not found.
        /// CONTENT: The specified tag.
        /// </returns>
        /// <example>
        /// GET: api/TagData/FindTag/5
        /// </example>
        [HttpGet]
        [ResponseType(typeof(Tag))]
        public IHttpActionResult FindTag(int id)
        {
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Updates an existing tag.
        /// </summary>
        /// <param name="id">The ID of the tag to update.</param>
        /// <param name="tag">The updated tag object.</param>
        /// <returns>
        /// HEADER: 204 (No Content) if successful; 404 (Not Found) if the tag doesn't exist.
        /// </returns>
        /// <example>
        /// POST: api/TagData/UpdateTag/5
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTag(int id, Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tag.TagId)
            {
                return BadRequest();
            }

            db.Entry(tag).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds a new tag to the system.
        /// </summary>
        /// <param name="tag">The tag to add.</param>
        /// <returns>
        /// HEADER: 201 (Created) with the location of the new resource.
        /// CONTENT: The newly created tag.
        /// </returns>
        /// <example>
        /// POST: api/TagData/AddTag
        /// </example>
        [ResponseType(typeof(Tag))]
        [HttpPost]
        public IHttpActionResult AddTag(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tags.Add(tag);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tag.TagId }, tag);
        }

        /// <summary>
        /// Deletes a tag from the system.
        /// </summary>
        /// <param name="id">The ID of the tag to delete.</param>
        /// <returns>
        /// HEADER: 200 (OK) if successful; 404 (Not Found) if the tag doesn't exist.
        /// </returns>
        /// <example>
        /// POST: api/TagData/DeleteTag/5
        /// </example>
        [ResponseType(typeof(Tag))]
        [HttpPost]
        public IHttpActionResult DeleteTag(int id)
        {
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }

            db.Tags.Remove(tag);
            db.SaveChanges();

            return Ok(tag);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TagExists(int id)
        {
            return db.Tags.Count(e => e.TagId == id) > 0;
        }
    }
}