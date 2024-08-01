using library.Models;
using Microsoft.AspNetCore.Mvc;
using library.DAL;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;

namespace library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult library()
        {
            List<Ark> arks = Data.Get.Arks.ToList();

            return View(arks);
        }

        public IActionResult CreateArc()
        {
            return View(new Ark());
        }

        [HttpPost]
        public IActionResult CreateArc(Ark ark)
        {
            if (ark == null)
            {
                NotFound();
            }

            Data.Get.Arks.Add(ark);
            Data.Get.SaveChanges();

            return RedirectToAction("library");
        }

        public IActionResult CreateShelf(int libraryId)
        {

            Ark? arks = Data.Get.Arks.FirstOrDefault(ark => ark.Id == libraryId);

            if (arks == null)
            {
               return NotFound();
            }

            return View(new Shelf { Ark = arks});
        }

        [HttpPost]
        public IActionResult CreateShelf(Shelf shelf)
        {
            if (shelf.ArkId12 == null)
            {
                return NotFound();
            }
            Ark? ark = Data.Get.Arks.FirstOrDefault(a => a.Id == shelf.ArkId12);

            if (ark == null)
            {
                return NotFound();
            }

            Shelf sel = new Shelf();

            sel.Height = shelf.Height;
            sel.Name = shelf.Name;
            sel.Ark = ark;
           // sel.ArkId = shelf.Id;

            Data.Get.Shelves.Add(sel);
            Data.Get.SaveChanges();

            return RedirectToAction("library");

        }


        public IActionResult CreteBoox(int libraryId)
        {
            List<Shelf> shelvesIncludeArk = Data.Get.Shelves.Include(shlfs => shlfs.Ark).ToList();

            List<Shelf> shelves = Data.Get.Shelves.Where(shel => shel.Ark.Id == libraryId).ToList();

            if (shelves.Count == 0)
            {
                return NotFound();
            }

            return View(shelves);
        }

        
        public IActionResult BookOnShelf(int id)
        {
            Shelf shelf = Data.Get.Shelves.FirstOrDefault(shelf => shelf.Id == id);

            if (shelf == null)
            {
                return NotFound();
            }

            return View(new Book { Shelf = shelf });
        }



        [HttpPost]
        public IActionResult BookOnShelf(Book book)
        {
            Shelf? shelfFromDb = Data.Get.Shelves.Include(shelf => shelf.Ark).FirstOrDefault(shelf => shelf.Id == book.BookOnShelf);

            if (shelfFromDb == null)
            {
                return NotFound();
            }

            Ark? arkFromDb = Data.Get.Arks.FirstOrDefault(ark => ark.Id == shelfFromDb.Ark.Id);

            if (arkFromDb == null)
            {
                return NotFound();
            }

            if ( (book.genre != arkFromDb.genre) || book.Height > shelfFromDb.Height)
            {
                return RedirectToAction("BookOnShelf");
            }

            Book neaBook = new Book();
            neaBook.genre = book.genre;
            neaBook.Height = book.Height;
            neaBook.Title = book.Title;
            neaBook.Shelf = shelfFromDb;


            Data.Get.Books.Add(neaBook);
            Data.Get.SaveChanges();


            if ((neaBook.Height + 10) < shelfFromDb.Height)
            {
                return RedirectToAction("GeaterThan10B");
            }

            return RedirectToAction("library");

        }

        public IActionResult GeaterThan10B() 
        {
            return View();
        }







        // ????? ?????
        public IActionResult Ex1CreateShelf(int id)
        {
           Ark arks = Data.Get.Arks.Include(ark => ark.Shelves).ToList().FirstOrDefault(a => a.Id == id);

            return View(arks);
        }
    }
}
