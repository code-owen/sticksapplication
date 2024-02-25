using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using SticksApplication.Models;

namespace SticksApplication.Controllers
{
    public class BlogPostController : Controller
    {
        // GET: BlogPost/List
        public ActionResult List()
        {
            //communicate with our BlogPost data api to retrieve a list of BlogPosts
            //https://localhost:44336/api/BlogPostData/ListBlogPosts

            HttpClient client = new HttpClient() { };

            string url = "https://localhost:44336/api/BlogPostData/ListBlogPosts";

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<BlogPostDto> blogPosts = response.Content.ReadAsAsync<IEnumerable<BlogPostDto>>().Result;

            return View(blogPosts);
        }

        // GET: BlogPost/Details/5
        public ActionResult Details(int id)
        {
            //communicate with our BlogPost data api to retrieve one BlogPost
            //https://localhost:44336/api/BlogPostData/FindBlogPost/{id}

            HttpClient client = new HttpClient() { };

            string url = "https://localhost:44336/api/BlogPostData/FindBlogPost/"+id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            BlogPostDto selectedBlogPost = response.Content.ReadAsAsync<BlogPostDto>().Result;

            return View(selectedBlogPost);
        }

        // GET: BlogPost/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPost/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogPost/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogPost/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogPost/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogPost/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
