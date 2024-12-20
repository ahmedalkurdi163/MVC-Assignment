using Application_Domain.Models;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Intefases;
using Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Classes
{
    public class TVShowRepository : ITVShowRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TVShowRepository> _logger;

        public TVShowRepository(AppDbContext context, ILogger<TVShowRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TVShow?> Update(int tvShowId, TVShowUpdateModel tvShow)
        {
            try
            {
                var item = await _context.TVShows.FirstOrDefaultAsync(t => t.TVShowId == tvShowId);
                if (item == null)
                {
                    _logger.LogWarning("TV show with ID {TVShowId} not found.", tvShowId);
                    return null;
                }

                item.ReleaseDate = tvShow.ReleaseDate;
                item.Title = tvShow.Title;
                item.URL = tvShow.URL;
                item.IsActive = true;
                item.Rating = tvShow.Rating;

                // Clear old languages and add new ones
                item.languages.Clear();
                foreach (var languageId in tvShow.languages)
                {
                    item.languages.Add(new TVShowLanguages { LanguageId = languageId.LanguageId });
                }

                _context.Update(item);
                await _context.SaveChangesAsync();
                _logger.LogInformation("TV show with ID {TVShowId} updated successfully.", tvShowId);

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the TV show with ID {TVShowId}.", tvShowId);
                return null;
            }
        }

        public async Task<TVShow?> Delete(int id)
        {
            try
            {
                var item = await _context.TVShows.FirstOrDefaultAsync(t => t.TVShowId == id);
                if (item == null)
                {
                    _logger.LogWarning("Attempted to delete a non-existent TV show with ID {TVShowId}.", id);
                    return null;
                }

                item.IsActive = false;
                _context.Update(item);
                await _context.SaveChangesAsync();
                _logger.LogInformation("TV show with ID {TVShowId} marked as inactive.", id);

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the TV show with ID {TVShowId}.", id);
                return null;
            }
        }

        public TVShow Get(int id, bool includeAttachment_img = false)
        {
            try
            {
                var query = _context.TVShows.Where(t => t.TVShowId == id && t.IsActive);
                if (includeAttachment_img)
                {
                    query = query.Include(t => t.Attachment_img);
                }

                var item = query.FirstOrDefault();
                if (item == null)
                {
                    _logger.LogWarning("TV show with ID {TVShowId} not found or is inactive.", id);
                }
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the TV show with ID {TVShowId}.", id);
                return null;
            }
        }

        public List<TVShow> All()
        {
            try
            {
                var items = _context.TVShows.Where(t => t.IsActive).Include(t => t.Attachment_img).ToList();
                _logger.LogInformation("Retrieved {Count} active TV shows.", items.Count);
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all active TV shows.");
                return new List<TVShow>();
            }
        }

        public async Task<TVShow?> Create(TVShowCreateModel tVShowforCreate)
        {
            if (tVShowforCreate == null)
            {
                _logger.LogWarning("Attempted to create a TV show with null data.");
                return null;
            }

            try
            {
                Attachment? attachment = null;

                if (tVShowforCreate.ImageFile != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", tVShowforCreate.ImageFile.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await tVShowforCreate.ImageFile.CopyToAsync(stream);
                    }

                    attachment = new Attachment
                    {
                        Title = Path.GetFileNameWithoutExtension(tVShowforCreate.ImageFile.FileName),
                        Path = "/uploads/" + tVShowforCreate.ImageFile.FileName,
                        FileType = GetFileType(tVShowforCreate.ImageFile.FileName)
                    };

                    _context.Attachments.Add(attachment);
                    await _context.SaveChangesAsync();
                }

                var tvShow = new TVShow
                {
                    Rating = tVShowforCreate.Rating,
                    ReleaseDate = tVShowforCreate.ReleaseDate,
                    Title = tVShowforCreate.Title,
                    URL = tVShowforCreate.URL,
                    AttachmentId = attachment.AttachmentId,
                    IsActive = true
                };

                foreach (var langId in tVShowforCreate.languages)
                {
                    var languageName = _context.Languages.FirstOrDefault(l => l.LanguageId == langId)?.Name;
                    if (languageName != null)
                    {
                        tvShow.languages.Add(new TVShowLanguages { LanguageId = langId, TVShowId = tvShow.TVShowId, LanguageName = languageName });
                    }
                }

                _context.TVShows.Add(tvShow);
                await _context.SaveChangesAsync();
                _logger.LogInformation("TV show {TVShowTitle} created successfully with ID {TVShowId}.", tvShow.Title, tvShow.TVShowId);
                return tvShow;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new TV show.");
                return null;
            }
        }

        private FileType GetFileType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" => FileType.Jpg,
                ".jpeg" => FileType.Jpg,
                ".png" => FileType.Png,
                ".gif" => FileType.Gif,
                ".bmp" => FileType.Bmp,
                ".tiff" => FileType.Tiff,
                _ => throw new NotSupportedException($"File type {extension} is not supported"),
            };
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                _logger.LogInformation("Database changes saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving database changes.");
            }
        }

        public List<Languages> GetLanguagesForTvShow(int? id)
        {
            try
            {
                var languages = _context.Languages
                                .Where(l => l.TVShows.Any(t => t.TVShowId == id))
                                .ToList();
                _logger.LogInformation("{Count} languages retrieved for TV show with ID {TVShowId}.", languages.Count, id);
                return languages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving languages for TV show with ID {TVShowId}.", id);
                return new List<Languages>();
            }
        }

        public async Task SaveChangesAcync()
        {
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Database changes saved asynchronously.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving database changes asynchronously.");
            }
        }
    }
}
