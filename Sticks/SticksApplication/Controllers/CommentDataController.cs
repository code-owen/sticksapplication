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
    public class CommentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of all comments.
        /// </summary>
        /// <returns>
        /// A collection of all comments in the database.
        /// </returns>
        /// <example>
        /// GET: api/CommentData/ListComments
        /// </example>
        [HttpGet]
        public IEnumerable<Comment> ListComments()
        {           
            return db.Comments;
        }

        /// <summary>
        /// Finds a specific comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to find.</param>
        /// <returns>
        /// HEADER: 200 (OK) if found; 404 (Not Found) if not found.
        /// CONTENT: The specified comment.
        /// </returns>
        /// <example>
        /// GET: api/CommentData/FindComment/5
        /// </example>
        [HttpGet]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult FindComment(int id)
        {
            Comment Comment = db.Comments.Find(id);
            if (Comment == null)
            {
                return NotFound();
            }

            return Ok(Comment);
        }

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="Comment">The updated comment object.</param>
        /// <returns>
        /// HEADER: 204 (No Content) if successful; 404 (Not Found) if the comment doesn't exist.
        /// </returns>
        /// <example>
        /// POST: api/BlogPostCommentData/UpdateBlogPostComment/5
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBlogPostComment(int id, Comment blogPostComment)
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
        [ResponseType(typeof(Comment))]
        [HttpPost]
        public IHttpActionResult AddBlogPostComment(Comment blogPostComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(blogPostComment);
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
        [ResponseType(typeof(Comment))]
        [HttpPost]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment blogPostComment = db.Comments.Find(id);
            if (blogPostComment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(blogPostComment);
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
            return db.Comments.Count(e => e.CommentId == id) > 0;
        }
    }
}