using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using SticksApplication.Models;

namespace SticksApplication.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment/List
        public ActionResult List()
        {
            //communicate with our Comment data api to retrieve a list of Comments
            //https://localhost:44336/api/CommentData/ListComments

            HttpClient client = new HttpClient() { };

            string url = "https://localhost:44336/api/CommentData/ListComments";

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<Comment> comments = response.Content.ReadAsAsync<IEnumerable<Comment>>().Result;

            return View(comments);
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            //communicate with our Comment data api to retrieve one Comment
            //https://localhost:44336/api/CommentData/FindComment/{id}

            HttpClient client = new HttpClient() { };

            string url = "https://localhost:44336/api/CommentData/FindComment/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<Comment> selectedComment = response.Content.ReadAsAsync<IEnumerable<Comment>>().Result;

            return View(selectedComment);
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comment/Create
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

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comment/Edit/5
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

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comment/Delete/5
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
