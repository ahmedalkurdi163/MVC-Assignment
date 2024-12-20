using Infrastructure.DbContexts;
using Infrastructure.Repositories.Intefases;
using Microsoft.AspNetCore.Mvc;
using MVC_App.Models;
using System.Diagnostics;

namespace MVC_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<TVShowsController> _logger;
        private readonly AppDbContext _context;
        private readonly ITVShowRepository _tvRepo;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly ILanguagesRepository _languagesRepository;

        public HomeController(ILogger<TVShowsController> logger, AppDbContext context,
                                  ITVShowRepository tvRepo,
                                  IAttachmentRepository attachmentRepository,
                                  ILanguagesRepository languagesRepository)
        {
            _logger = logger;
            _context = context;
            _tvRepo = tvRepo;
            _attachmentRepository = attachmentRepository;
            _languagesRepository = languagesRepository;
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
    }
}
