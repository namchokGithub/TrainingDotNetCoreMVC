﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Models;

namespace Movie.Controllers
{
    public class MovieController : Controller
    {
        MyProjectContext db = new MyProjectContext();
        public IActionResult Index()
        {
            var model = db.Movie.ToList();
            return View (model);
        }

        public string Welcome(string name, string id)
        {
            return $"Hello, Your name's {name}, your id's {id}";
        }

        public IActionResult Create()
        {
            return View();
        }

        /*
         * Insert data to database
         * Author: Nachok
         * Date: 2020-07-02
         */
        [HttpPost]
        public async Task<IActionResult> Create(MovieModel model, IFormFile fileUpload)
        {
            if (model.duration < 1)
            {
                ModelState.AddModelError("errDuration", "The duration field is required.");
                return View();
            }

            if (fileUpload == null)
            {
                ModelState.AddModelError("errFileUpload", "The file upload field is required.");
                return View();
            }

            if (ModelState.IsValid)
            {
                string pathImgMovie = "/image/movie/";
                string pathSave = $"wwwroot{pathImgMovie}";
                if (!Directory.Exists(pathSave))
                {
                    Directory.CreateDirectory(pathSave);
                }
                string extFile = Path.GetExtension(fileUpload.FileName);
                string fileName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + extFile;
                var path = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(stream);
                }

                DateTime dateNow = DateTime.Now;
                model.coverImg = pathImgMovie + fileName;
                model.createDate = dateNow;
                model.modifyDate = dateNow;
                db.Movie.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        /*
         * Insert data to database
         * Author: Nachok
         * Date: 2020-07-02
         */
        [HttpPost]
        public ActionResult Delete(int id)
        {
            MovieModel movie = db.Movie.Find(id);
            db.Movie.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
