using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CinemaCardFile.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaCardFile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CinemaLibContext db;

        public HomeController(ILogger<HomeController> logger, CinemaLibContext context)
        {
            _logger = logger;
            db = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int sizePage = 10; //задаем количество элементов на странице
            IEnumerable<Film> films = await db.Film.ToListAsync();
            var count = films.Count();
            var items = films.Skip((page - 1) * sizePage).Take(sizePage).ToList(); // пропускаем элементы и берем след. заданное количество

            PaginationModel paginModel = new PaginationModel(count, page, sizePage);
            IndexViewModel indexViewModel = new IndexViewModel
            {
                films = items,
                pageViewModel = paginModel
            };
            return View(indexViewModel);
        }

        public async Task<IActionResult> ViewFilm(int? id)
        {
            if (id != null)
            {
                Film film = await db.Film.FirstOrDefaultAsync(p => p.Id == id);
                if (film != null)
                {
                    return View(film);
                }
            }
            return NotFound();
        }

        public IActionResult AddFilm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFilm(FilmViewModel filmVM)
        {             
            Film film = new Film{               
                Name = filmVM.Name,
                Descrpion = filmVM.Descrpion,
                Year = filmVM.Year,
                Producer = filmVM.Producer,
                Username = filmVM.Username
            };
            //Записываем полученный файл в массив байтов
            if (filmVM.Poster != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new System.IO.BinaryReader(filmVM.Poster.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)filmVM.Poster.Length);
                }
                film.Poster = imageData;
            }
            db.Film.Add(film);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> EditFilm(int? id)
        {
            if (id != null)
            {
                Film film = await db.Film.FirstOrDefaultAsync(p => p.Id == id);
                if (film != null)
                {
                    return View(film);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditFilm(FilmViewModel filmVM)
        {
            Film film = new Film
            {
                Id = filmVM.Id,
                Name = filmVM.Name,
                Descrpion = filmVM.Descrpion,
                Year = filmVM.Year,
                Producer = filmVM.Producer,
                Username = filmVM.Username,
                Poster = db.Film.AsNoTracking().FirstOrDefault(p => p.Id == filmVM.Id).Poster
            };
            //Записываем полученный файл в массив байтов
            if (filmVM.Poster != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new System.IO.BinaryReader(filmVM.Poster.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)filmVM.Poster.Length);
                }
                film.Poster = imageData;
            }
            db.Film.Update(film);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }   
}
