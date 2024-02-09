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
    public class BlogPostCommentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of all blog post comments.
        /// </summary>
        /// <returns>
        /// A collection of all blog post comments in the database.
        /// </returns>
        /// <example>
        /// GET: api/BlogPostCommentData/ListBlogPostComments
        /// </example>
        [HttpGet]
        public IEnumerable<BlogPostComment> ListBlogPostComments()
        {           
            return db.BlogComments;
        }

        /// <summary>
        /// Finds a specific blog post comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the blog post comment to find.</param>
        /// <returns>
        /// HEADER: 200 (OK) if found; 404 (Not Found) if not found.
        /// CONTENT: The specified blog post comment.
        /// </returns>
        /// <example>
        /// GET: api/BlogPostCommentData/FindBlogPostComment/5
        /// </example>
        [HttpGet]
        [ResponseType(typeof(BlogPostComment))]
        public IHttpActionResult FindBlogPostComment(int id)
        {
            BlogPostComment blogPostComment = db.BlogComments.Find(id);
            if (blogPostComment == null)
            {
                return NotFound();
            }

            return Ok(blogPostComment);
        }

        /// <summary>
        /// Updates an existing blog post comment.
        /// </summary>
        /// <param name="id">The ID of the blog post comment to update.</param>
        /// <param name="blogPostComment">The updated blog post comment object.</param>
        /// <returns>
        /// HEADER: 204 (No Content) if successful; 404 (Not Found) if the blog post comment doesn't exist.
        /// </returns>
        /// <example>
        /// POST: api/BlogPostCommentData/UpdateBlogPostComment/5
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBlogPostComment(int id, BlogPostComment blogPostComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blogPostComment.CommentId)
            {
                return BadRequest();
            }

            db.Entry(blogPostComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostCommentExists(id))
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
        /// Adds a new blog post comment to the system.
        /// </summary>
        /// <param name="blogPostComment">The blog post comment to add.</param>
        /// <returns>
        /// HEADER: 201 (Created) with the location of the new resource.
        /// CONTENT: The newly created blog post comment.
        /// </returns>
        /// <example>
        /// POST: api/BlogPostCommentData/AddBlogPostComment
        /// </example>
        [ResponseType(typeof(BlogPostComment))]
        [HttpPost]
        public IHttpActionResult AddBlogPostComment(BlogPostComment blogPostComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BlogComments.Add(blogPostComment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = blogPostComment.CommentId }, blogPostComment);
        }

        /// <summary>
        /// Deletes a blog post comment from the system.
        /// </summary>
        /// <param name="id">The ID of the blog post comment to delete.</param>
        /// <returns>
        /// HEADER: 200 (OK) if successful; 404 (Not Found) if the blog post comment doesn't exist.
        /// </returns>
        /// <example>
        /// POST: api/BlogPostCommentData/DeleteBlogPostComment/5
        /// </example>
        [ResponseType(typeof(BlogPostComment))]
        [HttpPost]
        public IHttpActionResult DeleteBlogPostComment(int id)
        {
            BlogPostComment blogPostComment = db.BlogComments.Find(id);
            if (blogPostComment == null)
            {
                return NotFound();
            }

            db.BlogComments.Remove(blogPostComment);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlogPostCommentExists(int id)
        {
            return db.BlogComments.Count(e => e.CommentId == id) > 0;
        }
    }
}