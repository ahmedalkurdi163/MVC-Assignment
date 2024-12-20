using Application_Domain.Models;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Intefases;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Classes
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LanguagesRepository> _logger;

        public LanguagesRepository(AppDbContext context, ILogger<LanguagesRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Languages Add(Languages item)
        {
            try
            {
                if (item == null) return null;
                _context.Languages.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding language");
                return null;
            }
        }

        public List<Languages> All()
        {
            try
            {
                return _context.Languages.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all languages");
                return new List<Languages>();
            }
        }

        public Languages Delete(int id)
        {
            try
            {
                var language = _context.Languages.FirstOrDefault(l => l.LanguageId == id);
                if (language == null) return null;

                _context.Languages.Remove(language);
                _context.SaveChanges();
                return language;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting language with ID: {id}");
                return null;
            }
        }

        public Languages Get(int id)
        {
            try
            {
                var resulte =  _context.Languages.FirstOrDefault(l => l.LanguageId == id);
                return resulte;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving language with ID: {id}");
                return null;
            }
        }

        public Languages Update(Languages item)
        {
            try
            {
                if (item == null) return null;

                _context.Languages.Update(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating language with ID: {item.LanguageId}");
                return null;
            }
        }
    }
}
