﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Controllers
{
    public class FinderController : Controller
    {
        // GET: RecipeFinder
        public ActionResult Index()
        {
            return View();
        }

        // GET: RecipeFinder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecipeFinder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeFinder/Create
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

        // GET: RecipeFinder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecipeFinder/Edit/5
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

        // GET: RecipeFinder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecipeFinder/Delete/5
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
    }
}