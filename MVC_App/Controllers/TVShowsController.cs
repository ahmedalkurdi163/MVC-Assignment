using Application_Domain.Models;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Intefases;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_App.Controllers
{
    public class TVShowsController : Controller
    {
        private readonly ILogger<TVShowsController> _logger;
        private readonly AppDbContext _context;
        private readonly ITVShowRepository _tvRepo;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly ILanguagesRepository _languagesRepository;

        public TVShowsController(ILogger<TVShowsController> logger, AppDbContext context,
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

        // GET: TVShows
        public async Task<IActionResult> Index()
        {
            try
            {
                var items = _tvRepo.All();
                return View(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving TV shows list.");
                return View("Error");
            }
        }

        // GET: TVShows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("TV Show ID is null.");
                return NotFound();
            }

            try
            {
                var tvShow = _tvRepo.Get(id.Value, true);
                if (tvShow == null)
                {
                    _logger.LogWarning($"TV Show not found for ID {id.Value}.");
                    return NotFound();
                }

                ViewBag.Languages = _tvRepo.GetLanguagesForTvShow(id);
                _logger.LogInformation($"Displaying details for TV Show ID {id.Value}.");
                return View(tvShow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving details for TV Show ID {id.Value}.");
                return View("Error");
            }
        }

        // GET: TVShows/Create
        [Authorize]
        public IActionResult Create()
        {
            try
            {
                LoadRatings();
                LoadLanguages();
                ViewData["AttachmentId"] = new SelectList(_attachmentRepository.All().Result, "AttachmentId", "Path");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Create TV Show view.");
                return View("Error");
            }
        }

        // POST: TVShows/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TVShowCreateModel tvShowCreateModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = await _tvRepo.Create(tvShowCreateModel);
                    if (item != null)
                    {
                        _logger.LogInformation($"TV Show '{tvShowCreateModel.Title}' was created successfully.");
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating TV show.");
                    ModelState.AddModelError("", "There was an issue creating the TV show.");
                }
            }

            _logger.LogWarning("Invalid model state for TV show creation.");
            LoadRatings();
            LoadLanguages();
            ViewData["AttachmentId"] = new SelectList(_attachmentRepository.All().Result, "AttachmentId", "Path");
            return View(tvShowCreateModel);
        }

        // GET: TVShows/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Edit TV Show: ID is null.");
                return NotFound();
            }

            try
            {
                var tvShow = _tvRepo.Get(id.Value);
                if (tvShow == null)
                {
                    _logger.LogWarning($"TV Show not found for ID {id.Value}.");
                    return NotFound();
                }

                LoadRatings();
                LoadLanguages();
                ViewData["AttachmentId"] = new SelectList(_attachmentRepository.All().Result, "AttachmentId", "Path", tvShow.AttachmentId);
                return View(tvShow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading edit view for TV Show ID {id.Value}.");
                return View("Error");
            }
        }

        // POST: TVShows/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TVShowUpdateModel tvShowUpdateModel)
        {
            if (tvShowUpdateModel == null)
            {
                _logger.LogWarning("Edit TV Show: Update model is null.");
                return BadRequest("Invalid update model.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedTvShow = await _tvRepo.Update(id, tvShowUpdateModel);
                    if (updatedTvShow != null)
                    {
                        _logger.LogInformation($"TV Show with ID '{id}' was updated successfully.");
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error updating TV Show with ID '{id}'.");
                    ModelState.AddModelError("", "Failed to update the TV show.");
                }
            }

            _logger.LogWarning($"Invalid model state for TV show update with ID '{id}'.");
            LoadRatings();
            LoadLanguages();
            var tvShow = _tvRepo.Get(id);
            ViewData["AttachmentId"] = new SelectList(_attachmentRepository.All().Result, "AttachmentId", "Path", tvShow.AttachmentId);
            return View(tvShow);
        }

        // GET: TVShows/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Delete TV Show: ID is null.");
                return NotFound();
            }

            try
            {
                var tvShow = _tvRepo.Get(id.Value);
                if (tvShow == null)
                {
                    _logger.LogWarning($"TV Show not found for ID {id.Value}.");
                    return NotFound();
                }

                return View(tvShow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading delete view for TV Show ID {id.Value}.");
                return View("Error");
            }
        }

        // POST: TVShows/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var tvShow = _tvRepo.Get(id);
                if (tvShow != null)
                {
                    await _tvRepo.Delete(id);
                    await _tvRepo.SaveChangesAcync();
                    _logger.LogInformation($"TV Show with ID '{id}' was deleted successfully.");
                }
                else
                {
                    _logger.LogWarning($"Attempted to delete TV Show with ID '{id}' but it was not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting TV Show with ID '{id}'.");
                ModelState.AddModelError("", "There was an issue deleting the TV show.");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TVShowExists(int id)
        {
            try
            {
                return _tvRepo.All().Any(e => e.TVShowId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking if TV Show exists with ID '{id}'.");
                return false;
            }
        }

        private void LoadRatings()
        {
            try
            {
                var ratings = Enum.GetValues(typeof(Rating))
                                  .Cast<Rating>()
                                  .Select(r => new { Value = (int)r, Name = r.ToString() })
                                  .ToList();
                ViewBag.Ratings = new SelectList(ratings, "Value", "Name");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading ratings.");
            }
        }

        private void LoadLanguages()
        {
            try
            {
                var languages = _languagesRepository.All()
                                                    .Select(l => new { l.LanguageId, l.Name })
                                                    .ToList();
                ViewBag.Languages = new MultiSelectList(languages, "LanguageId", "Name");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading languages.");
            }
        }
    }
}
