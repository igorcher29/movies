using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movies.Repositories;
using Movies.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Movies.WebUI.Models;
using PagedList.Mvc;
using PagedList;

namespace Movies.WebUI.Controllers
{
    
    public class HomeController : Controller
    {
        IMovieRepository _repository;

        public HomeController(IMovieRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            //return View(_repository.GetMovies());
            ViewBag.UserId = CurrentUserId();
            return View(_repository.GetMovies().ToPagedList(pageNumber, pageSize));
        }

        // GET: Home/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Movie movie, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {                
                movie.UserId = CurrentUserId();
                _repository.Create(movie, image);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Home/Edit/3
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = _repository.GetMovie(id);
            if (movie==null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        // POST: Home/Edit/3
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Movie movie, HttpPostedFileBase image = null)
        {
            int t = 5;

            if (ModelState.IsValid)
            {
                movie.UserId = CurrentUserId();
                _repository.Update(movie, image);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private string CurrentUserId()
        {
            string userId = String.Empty;

            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);

            if (user != null)
            {
                userId = user.Id;
            }

            return userId;
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Movie movie = _repository.GetMovie(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = CurrentUserId();
            return View(movie);
        }

        public FileContentResult GetImage(int movieId)
        {
            Movie movie = _repository.GetMovie(movieId);
            if (movie != null)
            {
                return File(movie.ImageData, movie.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}