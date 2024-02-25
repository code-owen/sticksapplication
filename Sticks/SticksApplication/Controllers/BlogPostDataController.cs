using System;
using System.IO;
using System.Web;
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
using System.Diagnostics;

namespace SticksApplication.Controllers
{
    public class BlogPostDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns a list of all blog posts.
        /// </summary>
        /// <returns>
        /// A list of all blog posts in the database.
        /// </returns>
        /// <example>
        /// GET: api/BlogPostData/ListBlogPosts
        /// </example>
        [HttpGet]
        public IEnumerable<BlogPostDto> ListBlogPosts()
        {
            List<BlogPost> BlogPosts = db.Blogs.ToList();
            List<BlogPostDto> BlogPostDtos = new List<BlogPostDto>();

            BlogPosts.ForEach(b => BlogPostDtos.Add(new BlogPostDto()
            {
                BlogPostId = b.BlogPostId,
                Heading = b.Heading,
                Author = b.Author,
                PublishedDate = b.PublishedDate
            }));

            return BlogPostDtos;
        }


        /// <summary>
        /// Finds a specific blog post by its ID.
        /// </summary>
        /// <param name="id">The ID of the blog post to find.</param>
        /// <returns>
        /// HEADER: 200 (OK) if found; 404 (Not Found) if not found.
        /// CONTENT: The specified blog post.
        /// </returns>
        /// <example>
        /// GET: api/BlogPostData/FindBlogPost/5
        /// </example>
        [ResponseType(typeof(BlogPost))]
        [HttpGet]
        public IHttpActionResult FindBlogPost(int id)
        {
            BlogPost BlogPost = db.Blogs.Find(id);
            BlogPostDto BlogPostDto = new BlogPostDto()
            {
                BlogPostId=BlogPost.BlogPostId,
                Heading=BlogPost.Heading,
                Author=BlogPost.Author,
                PublishedDate=BlogPost.PublishedDate,
                Comments=BlogPost.Comments,
                Content=BlogPost.Content,
                ShortDescription=BlogPost.ShortDescription,
                FeaturedImageUrl=BlogPost.FeaturedImageUrl
            };
            if (BlogPost == null)
            {
                return NotFound();
            }

            return Ok(BlogPostDto);
        }

        /// <summary>
        /// Updates an existing blog post.
        /// </summary>
        /// <param name="id">The ID of the blog post to update.</param>
        /// <param name="BlogPost">The updated blog post object.</param>
        /// <returns>
        /// HEADER: 204 (No Content) if successful; 404 (Not Found) if the blog post doesn't exist.
        /// </returns>
        /// <example>
        /// POST: api/BlogPostData/UpdateBlogPost/5
        /// Form data: BlogPost json object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBlogPost(int id, BlogPost BlogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != BlogPost.BlogPostId)
            {
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + BlogPost.BlogPostId);
                return BadRequest();
            }

            db.Entry(BlogPost).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
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
        /// Adds a new blog post to the system.
        /// </summary>
        /// <param name="blogPost">The blog post to add.</param>
        /// <returns>
        /// HEADER: 201 (Created) with the location of the new resource.
        /// CONTENT: The newly created blog post.
        /// </returns>
        /// <example>
        /// POST: api/BlogPostsData/AddBlogPost
        /// Form data: BlogPost json object
        /// </example>
        [ResponseType(typeof(BlogPost))]
        [HttpPost]
        public IHttpActionResult AddBlogPost(BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Blogs.Add(blogPost);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = blogPost.BlogPostId }, blogPost);
        }

        /// <summary>
        /// Deletes a blog post from the system.
        /// </summary>
        /// <param name="id">The ID of the blog post to delete.</param>
        /// <returns>
        /// HEADER: 200 (OK) if successful; 404 (Not Found) if the blog post doesn't exist.
        /// </returns>
        /// <example>
        /// POST: api/BlogPostsData/DeleteBlogPost/5
        /// </example>
        [ResponseType(typeof(BlogPost))]
        [HttpPost]
        public IHttpActionResult DeleteBlogPost(int id)
        {
            BlogPost blogPost = db.Blogs.Find(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            db.Blogs.Remove(blogPost);
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

        private bool BlogPostExists(int id)
        {
            return db.Blogs.Count(e => e.BlogPostId == id) > 0;
        }
    }
}