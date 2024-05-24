
using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Exam_Mvcproject.Controllers
{
    public class HomeController : Controller
    {

        IExploreService _exploreService;

        public HomeController(IExploreService exploreService)
        {
            _exploreService = exploreService;
        }

        public IActionResult Index()
        {
            return View(_exploreService.GetAllExplore());
        }

       
    }
}