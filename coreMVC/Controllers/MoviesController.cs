using coreMVC.Models;
using coreMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NToastNotify;

namespace coreMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IToastNotification _toastNotification;
        private new List<string> _allowedEtentions = new List<string>() { ".jpg", ".png" };
        private long _maxPosterLengthMb = 1048576;
        public MoviesController(ApplicationDBContext context,IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movie.OrderByDescending(x=>x.Rate).Include(x=>x.Genre).ToListAsync();
            return View(movies);
        }

        public async Task<IActionResult> Create()
        {
            var ViewModel = new MovieFormViewModel
            {
                Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync()
            };
            return View("FormModel",ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormViewModel model)
        {
            model.Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync();
            if (!ModelState.IsValid)
            {
                model.Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync();
                return View("FormModel", model);
            }

            var files = Request.Form.Files;
            if (!files.Any())
            {
                model.Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Please Enter Poster");
                return View("FormModel", model);
            }
            
            var poster = files.FirstOrDefault();
            
            if (!_allowedEtentions.Contains(Path.GetExtension(poster.FileName).ToLower()))
            {
                model.Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Only jps and png allowed");
                return View("FormModel", model);
            }
            if(poster.Length > _maxPosterLengthMb)
            {
                model.Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Image Size Must Be Less than 1MB");
                return View("FormModel", model);
            }

            using var dataStream = new MemoryStream();
            poster.CopyToAsync(dataStream);

            var movie = new Movie()
            {
                Title = model.Title,
                Year = model.Year,
                Rate = model.Rate,
                Storyline = model.Storyline,
                Poster = dataStream.ToArray(),
                GenreID = model.GenreID
            };
            _context.Movie.Add(movie);
            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Movie Created Successfully");
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
                return NotFound();

            var viewModel = new MovieFormViewModel()
            {
                ID = movie.ID,
                GenreID = movie.GenreID,
                Title = movie.Title,
                Year = movie.Year,
                Rate = movie.Rate,
                Storyline = movie.Storyline,
                Poster = movie.Poster,
                Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync()

            };
            return View("FormModel", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(MovieFormViewModel model)
        {
            model.Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync();
            if (!ModelState.IsValid)
            {
                model.Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync();
                return View("FormModel", model);
            }

            var movie = await _context.Movie.FindAsync(model.ID);

            if (movie == null)
                return NotFound();

            var files = Request.Form.Files;
            if (files.Any())
            {
                var poster = files.FirstOrDefault();

                using var dataStream = new MemoryStream();
                await poster.CopyToAsync(dataStream);
                model.Poster = dataStream.ToArray();

                if (!_allowedEtentions.Contains(Path.GetExtension(poster.FileName).ToLower()))
                {
                    model.Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync();
                    ModelState.AddModelError("Poster", "Only jps and png allowed");
                    return View("FormModel", model);
                }
                if (poster.Length > _maxPosterLengthMb)
                {
                    model.Genres = await _context.Genre.OrderBy(x => x.Name).ToListAsync();
                    ModelState.AddModelError("Poster", "Image Size Must Be Less than 1MB");
                    return View("FormModel", model);
                }

                movie.Poster = model.Poster;
            }

            movie.GenreID = model.GenreID;
            movie.Title = model.Title;
            movie.Year = model.Year;
            movie.Rate = model.Rate;
            movie.Storyline = model.Storyline;

            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Movie Updated Successfully");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = await _context.Movie.Include(x=>x.Genre).SingleOrDefaultAsync(x=>x.ID == id);
            if (movie == null)
                return NotFound();
            return View(movie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
                return NotFound();

            _context.Movie.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }
    }
}
