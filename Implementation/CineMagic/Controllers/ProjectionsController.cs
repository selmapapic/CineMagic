using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMagic.Controllers
{
    public class ProjectionsController : Controller
    {
        // GET: ProjectionsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProjectionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProjectionsController/Create
        public ActionResult AddProjection()
        {
            return View();
        }

        // POST: ProjectionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectionsController/Edit/5
        public ActionResult EditProjection(int id)
        {
            return View();
        }

        // POST:ProjectionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET:ProjectionsController/Delete/5
        public ActionResult DeleteProjection(int id)
        {
            return View();
        }

        // POST: ProjectionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult HomeAdmin()
        {
            return RedirectToAction("HomeAdmin", "Administrator");
        }
    }
}
