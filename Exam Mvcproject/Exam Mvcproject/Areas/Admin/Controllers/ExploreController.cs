using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.Security.Policy;

namespace Exam_Mvcproject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExploreController : Controller
    {
        private readonly IExploreService _exploreService;

        public ExploreController(IExploreService exploreService)
        {
            _exploreService = exploreService;
        }

        public IActionResult Index()
        { List<Explore> explores = _exploreService.GetAllExplore();
            return View(explores);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Explore explore)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _exploreService.AddExplore(explore);
            } 
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error");
                return View();
            }
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Update(int id)
        {
            var oldexplore = _exploreService.GetExplore(x => x.Id == id);
            if (oldexplore == null)
            {
                ModelState.AddModelError("", "Explore in not null");
                return View();
            }
            return View(oldexplore);
        }
        [HttpPost]
        public IActionResult Update(Explore newexplore)
        {
            if (!ModelState.IsValid)
            {
                return View(); 
            }
            try
            {
                _exploreService.UpdateExplore(newexplore.Id, newexplore);  
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error");
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            _exploreService.RemoveExplore(id);
            return RedirectToAction(nameof(Index));
        }
    }     

    
}
