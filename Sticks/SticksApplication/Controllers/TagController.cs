using SticksApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SticksApplication.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag/List
        public ActionResult List()
        {
            //communicate with our Tag data api to retrieve a list of Tags
            //https://localhost:44336/api/TagData/ListTags

            HttpClient client = new HttpClient() { };

            string url = "https://localhost:44336/api/TagData/ListTags";

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<Tag> tags = response.Content.ReadAsAsync<IEnumerable<Tag>>().Result;
            Debug.WriteLine("Number of animals received");
            Debug.WriteLine(tags.Count());

            return View(tags);
        }

        // GET: Tag/Details/5
        public ActionResult Details(int id)
        {
            //communicate with our Tag data api to retrieve one Tag
            //https://localhost:44336/api/TagData/FindTag/{id}

            HttpClient client = new HttpClient() { };

            string url = "https://localhost:44336/api/TagData/FindTag/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<Tag> selectedTag = response.Content.ReadAsAsync<IEnumerable<Tag>>().Result;

            return View(selectedTag);
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tag/Create
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

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tag/Edit/5
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

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tag/Delete/5
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
